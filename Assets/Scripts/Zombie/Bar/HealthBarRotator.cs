using DanyExtensions;
using UnityEngine;

public class HealthBarRotator : MonoBehaviour
{
    private Transform _camera;
    
    public void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
         Vector3 angles = Quaternion.LookRotation(_camera.forward).eulerAngles;
         transform.rotation = Quaternion.Euler(0, angles.y, angles.z);
    }
}
