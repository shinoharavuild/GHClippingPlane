using Rhino.Geometry;
using Rhino.DocObjects;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;

namespace GHClippingPlane
{
    public class ClippingPlaneGoo : GH_GeometricGoo<ClippingPlaneSurface>
    {
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
        public ClippingPlaneGoo(ObjRef objRef)
        {
            this.ReferenceID = objRef.ObjectId;
            this.m_value = (objRef.Object() as ClippingPlaneObject).ClippingPlaneGeometry;
        }
        public override BoundingBox Boundingbox => throw new System.NotImplementedException();
        public override string TypeDescription => throw new System.NotImplementedException();
        public override string TypeName => ObjectType.ClipPlane.ToString();
        public override Guid ReferenceID { get => base.ReferenceID; set => base.ReferenceID = value; }
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
            if (this.ReferenceID != default)
            {
                return this.TypeName;
            }
            else
            {
                return $"Referenced {this.TypeName}";
            }
        }
        public override IGH_GeometricGoo Transform(Transform xform)
        {
            throw new System.NotImplementedException();
        }
    }
}
