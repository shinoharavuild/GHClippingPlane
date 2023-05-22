using Rhino;
using Rhino.Geometry;
using Rhino.DocObjects;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;

namespace GHClippingPlane
{
    public class ClippingPlaneGoo : GH_GeometricGoo<ClippingPlaneSurface>, IGH_PreviewData
    {
        #region Constructor
        public ClippingPlaneGoo()
        {
            this.ReferenceID = default;
            this.m_value = default;
        }
        public ClippingPlaneGoo(ClippingPlaneSurface clippingPlaneSurface)
        {
            this.ReferenceID = default;
            this.m_value = clippingPlaneSurface;
        }
        public ClippingPlaneGoo(Guid id)
        {
            this.ReferenceID = id;
            this.m_value = (RhinoDoc.ActiveDoc.Objects.FindId(id) as ClippingPlaneObject).ClippingPlaneGeometry;
        }
        #endregion

        #region Implementation of GH_GeometricGoo
        public override BoundingBox Boundingbox => this.m_value.GetBoundingBox((this.m_value as ClippingPlaneSurface).Plane);
        public override bool IsReferencedGeometry => this.ReferenceID != default;
        public override Guid ReferenceID
        {
            get => base.ReferenceID;
            set => base.ReferenceID = value;
        }
        public override string TypeDescription => throw new System.NotImplementedException();
        public override string TypeName => ObjectType.ClipPlane.ToString();
        public override IGH_GeometricGoo DuplicateGeometry()
        {
            return new ClippingPlaneGoo(this.m_value);
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
            throw new System.NotImplementedException();
        }
        #endregion

        #region Implementation of IGH_PreviewData
        public BoundingBox ClippingBox => this.Boundingbox;
        public void DrawViewportWires(GH_PreviewWireArgs args)
        {
            PlaneSurface planeSrf = (this.m_value as ClippingPlaneSurface) as PlaneSurface;
            GH_Surface gh_srf = new GH_Surface(planeSrf);
            GH_Rectangle gh_rect = new GH_Rectangle();
            gh_rect.CastFrom(gh_srf);

            gh_rect.DrawViewportWires(args);


        }
        public void DrawViewportMeshes(GH_PreviewMeshArgs args) { }
        #endregion
    }
}
