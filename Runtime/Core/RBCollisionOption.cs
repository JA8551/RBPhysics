using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RBPhys
{
    public enum RBCollisionOption
    {
        Both,
        Ignore
    }

    public enum RBCollisionLayerOption
    {
        IgnorePhyscast = 1,
        Trigger = 2,
        AllowSoftClip = 3,
        ForceSoftClip = 4
    }
}