using Rhino.Geometry;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;

namespace GHClippingPlane
{
    public class ClippingPlaneGoo : GH_GeometricGoo<ClippingPlaneSurface>
    {
        #region 継承Properties
        public override BoundingBox Boundingbox => throw new System.NotImplementedException();
        public override string TypeDescription => throw new System.NotImplementedException();
        public override string TypeName => throw new System.NotImplementedException();
        #endregion
        #region 継承Method
        public override IGH_GeometricGoo DuplicateGeometry()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
        public override IGH_GeometricGoo Transform(Transform xform)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
