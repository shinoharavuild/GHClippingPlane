using Rhino;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using Rhino.DocObjects;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;

namespace GHClippingPlane
{
    public class ClippingBoxGoo : GH_Box
    {
        private List<Guid> ViewportIDs { get; }
        public bool BakeGeometry(RhinoDoc doc, ObjectAttributes att, out Guid obj_guid)
        {
            List<Guid> ids = new List<Guid>();
            foreach (BrepFace face in this.Value.ToBrep().Faces)
            {
                GH_Rectangle gh_rect = new GH_Rectangle();
                gh_rect.CastFrom(face);
                Guid id = doc.Objects.AddClippingPlane(gh_rect.Value.Plane, gh_rect.Value.Width, gh_rect.Value.Height, ViewportIDs, att);
                ids.Add(id);
            }
            doc.Groups.Add("ClippingBox",ids);
            obj_guid = Guid.Empty;
            return true;
        }
    }
}
