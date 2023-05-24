using Rhino;
using Rhino.Geometry;
using Rhino.DocObjects;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GHClippingPlane
{
    public class ClippingPlaneGoo : GH_GeometricGoo<ClippingPlaneSurface>, IGH_PreviewData, IGH_BakeAwareData
    {
        private List<Guid> ViewportIDs { get; }
        #region Constructor
        public ClippingPlaneGoo() { }
        public ClippingPlaneGoo(ClippingPlaneSurface clippingPlaneSurface)
        {
            this.Value = clippingPlaneSurface;
            this.ViewportIDs = default;
        }
        public ClippingPlaneGoo(Guid id)
        {
            this.ReferenceID = id;
            this.ViewportIDs = this.Value.ViewportIds().ToList();
        }
        #endregion

        #region Implementation of GH_GeometricGoo
        public override BoundingBox Boundingbox => this.Value.GetBoundingBox((this.Value as ClippingPlaneSurface).Plane);
        public override bool IsReferencedGeometry => this.ReferenceID != Guid.Empty;
        public override Guid ReferenceID { get; set; }
        public override string TypeDescription => throw new System.NotImplementedException();
        public override string TypeName => ObjectType.ClipPlane.ToString();
        public override IGH_GeometricGoo DuplicateGeometry()
        {
            if (this.IsReferencedGeometry)
                return new ClippingPlaneGoo(this.ReferenceID);
            else
                return new ClippingPlaneGoo(this.Value);
        }
        public override BoundingBox GetBoundingBox(Transform xform)
        {
            throw new System.NotImplementedException();
        }
        public override IGH_GeometricGoo Morph(SpaceMorph xmorph)
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            if (this.IsReferencedGeometry)
            {
                return $"Referenced {this.TypeName}";
            }
            else
            {
                return this.TypeName;
            }
        }
        public override IGH_GeometricGoo Transform(Transform xform)
        {
            ClippingPlaneGoo duplicated = new ClippingPlaneGoo(this.Value);
            if (xform == null)
                return null;
            else
            {
                duplicated.Value.Transform(xform);
                return duplicated;
            }
        }
        public override ClippingPlaneSurface Value
        {
            get
            {
                if (this.IsReferencedGeometry)
                {
                    RhinoObject rhobj = RhinoDoc.ActiveDoc.Objects.FindId(this.ReferenceID);
                    return (rhobj as ClippingPlaneObject).ClippingPlaneGeometry;
                }
                else
                    return this.m_value;
            }
            set=> this.m_value = value;
        }
        #endregion

        #region Implementation of IGH_PreviewData
        public BoundingBox ClippingBox => this.Boundingbox;
        public void DrawViewportWires(GH_PreviewWireArgs args)
        {
            GH_Rectangle gh_rect = new GH_Rectangle();
            gh_rect.CastFrom(new GH_Surface(this.Value));

            gh_rect.DrawViewportWires(args);

            Line[] lines =
            {
                new Line(gh_rect.Value.PointAt(0.5,0),gh_rect.Value.PointAt(0.5,1)),
                new Line(gh_rect.Value.PointAt(0.5,0.5),gh_rect.Value.PointAt(0,0.5)),
                new Line(gh_rect.Value.PointAt(0.5, 0.5), gh_rect.Value.PointAt(0.5, 0.5) + gh_rect.Value.Plane.Normal * gh_rect.Value.Width / 2),
             };
            foreach (Line line in lines)
                new GH_Line(line).DrawViewportWires(args);
        }
        public void DrawViewportMeshes(GH_PreviewMeshArgs args)
        {
            PlaneSurface planeSrf = (this.Value as ClippingPlaneSurface) as PlaneSurface;
            GH_Surface gh_srf = new GH_Surface(planeSrf);
            gh_srf.DrawViewportMeshes(args);
        }
        #endregion

        #region Implementation of IGH_BakeAwareData
        public bool BakeGeometry(RhinoDoc doc, ObjectAttributes att, out Guid obj_guid)
        {
            GH_Rectangle gh_rect = new GH_Rectangle();
            gh_rect.CastFrom(new GH_Surface(this.Value));
            obj_guid = doc.Objects.AddClippingPlane(gh_rect.Value.Plane, gh_rect.Value.Width, gh_rect.Value.Height, ViewportIDs,att);
            return true;
        }

        #endregion
    }
}
