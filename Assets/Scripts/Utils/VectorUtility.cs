using UnityEngine;

namespace Utils
{
    public static class VectorUtility 
    {

        public static Vector3 GetPointOnCircle(Vector2 positionOnScreen, Vector3 center, int radius)
        {
            var cameraForward = Camera.main.gameObject.transform.forward;
            
            var ray = Camera.main.ScreenPointToRay(positionOnScreen);
            var plane = new Plane( cameraForward, center);

            if (!plane.Raycast(ray, out float enter))
                return default;

            var point = ray.GetPoint(enter);

            return center + Vector3.ClampMagnitude(point - center, radius);
        }
        
    }
}