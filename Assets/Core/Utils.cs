using System.Collections;
using UnityEngine;

namespace Util
{
    public class UtilImpl
    {
        static public float VectorLength2(Vector2 target)
        {
            float pow = target.x * target.x + target.y * target.y;
            return Mathf.Sqrt(pow);
        }
        static public float VectorLength3(Vector3 target)
        {
            float pow = target.x * target.x + target.y * target.y + target.z * target.z;
            return Mathf.Sqrt(pow);
        }

        static public bool IsSameLayer(int layer, LayerMask mask)
        {
            return (mask.value & (1 << layer)) > 0;
        }
    }
}