using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using UnityEditorInternal;
using UnityEngine;

namespace RBPhys
{
    public class RBCapsuleCollider : RBCollider
    {
        const RBGeometryType GEOMETRY_TYPE = RBGeometryType.Capsule;

        [SerializeField] Vector3 _center = Vector3.zero;
        [SerializeField] Vector3 _rotationEuler = Vector3.zero;
        [SerializeField] float _radius = 0.5f;
        [SerializeField] float _height = 1f;

        public override RBGeometryType GeometryType { get { return GEOMETRY_TYPE; } }

        Vector3 Center { get { return _center; } set { _center = value; } }
        Quaternion LocalRot { get { return Quaternion.Euler(_rotationEuler); } set { _rotationEuler = value.eulerAngles; } }
        float Radius { get { return _radius; } set { _radius = Mathf.Abs(value); } }
        float Height { get { return _height; } set { _height = Mathf.Abs(value); } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float CalcVolume()
        {
            float r = _radius;
            float vSphere = (4f * Mathf.PI * r * r * r) / 3f;
            float vCylinder = (Mathf.PI * r * r) * _height;

            return vSphere + vCylinder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderSphere CalcSphere(Vector3 pos, Quaternion rot)
        {
            return new RBColliderSphere(pos + _center, _height + _radius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderAABB CalcAABB(Vector3 pos, Quaternion rot)
        {
            var p = GetEdge(pos, rot * LocalRot);

            Vector3 r = Vector3.one * _radius;

            Vector3 min = Vector3.Min(p.begin, p.end) - r;
            Vector3 max = Vector3.Max(p.begin, p.end) + r;

            return new RBColliderAABB((min + max) / 2f, max - min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderOBB CalcOBB(Vector3 pos, Quaternion rot)
        {
            return new RBColliderOBB(pos + _center, rot * LocalRot, new Vector3(_radius * 2, _height, _radius * 2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetColliderCenter(Vector3 pos, Quaternion rot)
        {
            return pos + _center;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (Vector3 begin, Vector3 end) GetEdge(Vector3 pos, Quaternion rot)
        {
            Quaternion r = rot * LocalRot;
            float p = (_height + _radius) / 2f;
            return (pos + _center + (r * new Vector3(0, p, 0)), pos + _center + (r * new Vector3(0, -p, 0)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RBColliderCapsule CalcCapsule(Vector3 pos, Quaternion rot)
        {
            return new RBColliderCapsule(pos, rot * LocalRot, _radius, _height);
        }
    }
}