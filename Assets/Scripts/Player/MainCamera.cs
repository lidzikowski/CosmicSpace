using UnityEngine;

public class MainCamera : MonoBehaviour {

    public static GameObject CameraTarget;

    [Header("Camera Settings")]
    public Vector3 CameraDistance;
    public float CameraSpeed;

    void LateUpdate()
    {
        if (CameraTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, CameraTarget.transform.position + CameraDistance, CameraSpeed);
        }
    }
}