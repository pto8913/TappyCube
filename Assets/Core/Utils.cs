using System.Collections;
using Unity.VisualScripting;
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

        static public float MapRange(float value, float input_start, float input_end, float output_start, float output_end)
        {
            float slope = 1f * (output_end - output_start) / (input_end - input_start);
            return output_start + Mathf.Round(slope * (value - input_start));
        }
    }
}