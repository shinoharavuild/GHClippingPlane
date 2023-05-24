using Rhino;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.DocObjects;
using Rhino.Geometry;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GHClippingPlane
{
    public class ClippingBoxParameter : GH_PersistentGeometryParam<ClippingBoxGoo>, IGH_PreviewObject, IGH_BakeAwareObject
    {
        public ClippingBoxParameter() : base(new GH_InstanceDescription("Clipping Box", "CB", "", "Params", "Geometry")) { }
        
        #region Implementation of GH_PersistentGeometryParam
        public override Guid ComponentGuid => new Guid("F7696DDA-BF6B-4FBE-9450-95208C6831B3");
        protected override GH_GetterResult Prompt_Singular(ref ClippingBoxGoo value)
        {
            RhinoGet.GetBox(out Box box);
            value = new ClippingBoxGoo(new GH_Box(box));
            return GH_GetterResult.success;
        }
        protected override GH_GetterResult Prompt_Plural(ref List<ClippingBoxGoo> values)
        {
            return GH_GetterResult.cancel;
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

        #region Implementation of IGH_BakeAwareObject
        public bool IsBakeCapable => true;
        public void BakeGeometry(RhinoDoc doc, List<Guid> obj_ids)
        {
            this.BakeGeometry(doc, null, obj_ids);
        }
        public void BakeGeometry(RhinoDoc doc, ObjectAttributes att, List<Guid> obj_ids)
        {
            if (att == null)
                att = doc.CreateDefaultAttributes();
            foreach (ClippingBoxGoo value in this.m_data)
            {
                ClippingBoxGoo dup = new ClippingBoxGoo(value);
                dup.BakeGeometry(doc,att,out Guid guid);
                obj_ids.Add(guid);
            }
        }
        #endregion
    }
}
