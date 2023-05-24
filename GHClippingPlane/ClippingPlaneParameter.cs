using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.DocObjects;
using Rhino.Geometry;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GHClippingPlane
{
    public class ClippingPlaneParameter : GH_PersistentGeometryParam<ClippingPlaneGoo>, IGH_PreviewObject
    {
        public ClippingPlaneParameter() : base(new GH_InstanceDescription("Clipping Plane", "CP", "", "Params", "Geometry")) { }
        
        #region Implementation of GH_PersistentGeometryParam
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
                    value = new ClippingPlaneGoo(getObject.Object(0).ObjectId);
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
                    values = getObject.Objects().Select(obj => new ClippingPlaneGoo(obj.ObjectId)).ToList();
                    return GH_GetterResult.success;
                case GetResult.Nothing:
                    return GH_GetterResult.accept;
                default:
                    return GH_GetterResult.cancel;
            }
        }
        #endregion

        #region Implementation of IGH_PreviewObject
        public bool Hidden { get; set; }
        public bool IsPreviewCapable => true;
        public BoundingBox ClippingBox => base.Preview_ComputeClippingBox();
        public void DrawViewportMeshes(IGH_PreviewArgs args)
        {
            base.Preview_DrawMeshes(args);
        }
        public void DrawViewportWires(IGH_PreviewArgs args)
        {
            base.Preview_DrawWires(args);
        }
        #endregion
    }
}
