using UnityEngine;

namespace Utils
{
    public static class VectorUtility 
    {

        public static Vector3 GetPointOnCircle(float angleDegrees, Vector3 center, float radius, Vector3 up, Vector3 right)
        {
            var initial =  Vector3.Cross(up, right) * radius;
            return  Quaternion.AngleAxis(angleDegrees, up) * initial + center;
        }
        
    }
}