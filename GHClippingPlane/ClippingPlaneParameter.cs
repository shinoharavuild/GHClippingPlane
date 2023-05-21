using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.DocObjects;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GHClippingPlane
{
    public class ClippingPlaneParameter : GH_PersistentGeometryParam<ClippingPlaneGoo>
    {
        public ClippingPlaneParameter() : base(new GH_InstanceDescription("Clipping Plane", "CP", "", "Params", "Geometry")) { }

        public override Guid ComponentGuid => new Guid("325B5A72-A52B-41B9-B28D-CFF715848425");
        protected override GH_GetterResult Prompt_Singular(ref ClippingPlaneGoo value)
        {
            GetObject getObject = new GetObject();
            getObject.SetCommandPrompt("Select a Clip Plane.");
            getObject.AcceptNothing(true);
            getObject.GeometryFilter = ObjectType.ClipPlane;

            switch (getObject.Get())
            {
                case GetResult.Object:
                    value = new ClippingPlaneGoo(getObject.Object(0));
                    return GH_GetterResult.success;
                case GetResult.Nothing:
                    return GH_GetterResult.accept;
                default:
                    return GH_GetterResult.cancel;
            }
        }
        protected override GH_GetterResult Prompt_Plural(ref List<ClippingPlaneGoo> values)
        {
            GetObject getObject = new GetObject();
            getObject.SetCommandPrompt("Select Clip Planes.");
            getObject.AcceptNothing(true);
            getObject.GeometryFilter = ObjectType.ClipPlane;

            switch (getObject.GetMultiple(1,0))
            {
                case GetResult.Object:
                    values = getObject.Objects().Select(obj => new ClippingPlaneGoo(obj)).ToList();
                    return GH_GetterResult.success;
                case GetResult.Nothing:
                    return GH_GetterResult.accept;
                default:
                    return GH_GetterResult.cancel;
            }
        }
    }
}
