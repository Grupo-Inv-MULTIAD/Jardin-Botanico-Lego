using UnityEngine;

public class miradaCamera : MonoBehaviour
{
    public Camera _cam;
    public Transform lookAt;  // arrastra aquí el personaje
    public float sensitivity = 1.0f;
    public float distance = 4.0f;
    public float height = 2.0f;

    private float currentX = 0;
    private float currentY = 0;

    void LateUpdate()
    {
        if (lookAt == null || _cam == null)
        {
            Debug.LogWarning("Faltan referencias en miradaCamera.cs");
            return;
        }

        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;
        currentY = Mathf.Clamp(currentY, -20, 60);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 dir = new Vector3(0, 0, -distance);
        Vector3 targetPos = lookAt.position + rotation * dir + Vector3.up * height;

        _cam.transform.position = targetPos;
        _cam.transform.LookAt(lookAt.position + Vector3.up * 1.5f);
    }

    public Camera GetCamera()
    {
        return _cam ? _cam : Camera.main;
    }
}
