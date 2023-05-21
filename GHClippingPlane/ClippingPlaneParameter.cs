using Rhino.Geometry;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;

namespace GHClippingPlane
{
    public class ClippingPlaneParameter : GH_PersistentGeometryParam<ClippingPlaneGoo>
    {
        public ClippingPlaneParameter() : base(new GH_InstanceDescription("Clipping Plane", "CP", "", "Params", "Geometry")) { }

        public override Guid ComponentGuid => new Guid("325B5A72-A52B-41B9-B28D-CFF715848425");
        protected override GH_GetterResult Prompt_Singular(ref ClippingPlaneGoo value)
        {
            throw new NotImplementedException();
        }
        protected override GH_GetterResult Prompt_Plural(ref List<ClippingPlaneGoo> values)
        {
            throw new NotImplementedException();
        }
    }
}
