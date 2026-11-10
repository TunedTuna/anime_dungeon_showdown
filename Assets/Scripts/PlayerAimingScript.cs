using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimingScript : MonoBehaviour
{
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        RotateToMouse();      //aimObject
    }

    void RotateToMouse()
    {
        Vector3 lookDirection = GetAimDirection();
        if (lookDirection.sqrMagnitude > 0.0001f)
        {
            //instead of rotating the object, have this communicate with the animator to know what sprite to use
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    public Vector3 GetAimDirection()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null)
            return transform.forward;

        Vector2 mousePosition = Mouse.current != null
            ? Mouse.current.position.ReadValue()
            : (Vector2)Input.mousePosition;

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0f, transform.position.y, 0f));

        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 lookDirection = point - transform.position;
            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.0001f)
                return lookDirection.normalized;
        }

        return transform.forward;
    }
}
