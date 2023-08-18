using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RebelGameDevs.Utils.World
{
    public static class RGD_Vector3Methods
    {
        public static Vector4 RebelGameDevsAbsoluteVector(Vector4 vector)
        {
            return new Vector4(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z), Mathf.Abs(vector.w));
        }
        public static Vector3 RebelGameDevsAbsoluteVector(Vector3 vector)
        {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }
        public static Vector2 RebelGameDevsAbsoluteVector(Vector2 vector)
        {
            return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }
    }
}