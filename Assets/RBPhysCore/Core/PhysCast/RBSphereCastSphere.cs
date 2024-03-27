﻿using RBPhys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RBPhys.RBPhysCore;

public static partial class RBSphereCast
{
    public static class SphereCastSphere
    {
        public static RBColliderCastHitInfo CalcRayCollision(RBColliderSphere sphere, Vector3 org, Vector3 dirN, float length, float radius)
        {
            Vector3 p = sphere.pos - org;
            float b = Vector3.Dot(dirN, p);
            float c = Vector3.Dot(p, p) - (sphere.radius * sphere.radius);

            float b2 = b * b;
            float s = b2 - c;
            if (s < 0)
            {
                return default;
            }

            s = Mathf.Sqrt(s);

            float t1 = b - s;
            float t2 = b + s;

            float t = t1;

            if (!(t > 0 && t <= length) || (t2 > 0 && t2 <= length && t2 < t))
            {
                t = t2;
            }

            if (t > 0 && t <= length)
            {
                Vector3 pos = org + dirN * t;
                Vector3 n = ((pos - sphere.pos) / sphere.radius).normalized;

                RBColliderCastHitInfo info = new RBColliderCastHitInfo();
                info.SetHit(pos - n * radius, n, t);

                return info;
            }

            return default;
        }
    }
}