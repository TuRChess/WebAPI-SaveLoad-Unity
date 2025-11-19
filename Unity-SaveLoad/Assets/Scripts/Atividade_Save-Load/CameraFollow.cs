using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance, height, rotationSpeed;

    private float currentRotationX;
    private float currentRotationY;

    void LateUpdate()
    {
        if (!target) return;

        currentRotationX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentRotationY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentRotationY = Mathf.Clamp(currentRotationY, -20f, 60f);

        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 offset = rotation * new Vector3(0, height, -distance);
        transform.position = target.position + offset;

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
