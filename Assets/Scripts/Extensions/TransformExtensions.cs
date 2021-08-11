using UnityEngine;

namespace DanyExtensions
{
    public static class TransformExtensions
    {
        public static void RotateTowards(this Transform transform, Vector3 target, float maxDegreesDelta)
        {
            Quaternion now = transform.rotation;
            Quaternion need = Quaternion.LookRotation(target - transform.position);  
            transform.rotation = Quaternion.RotateTowards(now, need, maxDegreesDelta);
        }
        
        public static float AngleTo(this Transform transform, Vector3 target)
        {
            Quaternion now = transform.rotation;
            Quaternion need = Quaternion.LookRotation(target - transform.position);  
            return Quaternion.Angle(now, need);
        }
    }
}