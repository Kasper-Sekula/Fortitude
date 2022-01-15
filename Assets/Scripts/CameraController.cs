using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraMovementSpeed = 30f;
    [SerializeField] float marginOfCameraMouseMovement = 20f;
    [SerializeField] float scrollSpeed = 10f;
    [SerializeField] Vector2 cameraMovementLimit;
    [SerializeField] float scrollUpperLimit;
    [SerializeField] float scrollBottomLimit;
    [SerializeField] float cameraCenterOffset;

    private void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        Vector3 cameraPosition = transform.position;

        cameraPosition = ChangeCameraPosition(cameraPosition);
        SetNewCameraPosition(cameraPosition);
    }

    private Vector3 ChangeCameraPosition(Vector3 cameraPosition)
    {
        if (Input.GetKey("w") || Input.mousePosition.y > Screen.height - marginOfCameraMouseMovement)
        {
            cameraPosition.z += cameraMovementSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= marginOfCameraMouseMovement)
        {
            cameraPosition.z -= cameraMovementSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - marginOfCameraMouseMovement)
        {
            cameraPosition.x += cameraMovementSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= marginOfCameraMouseMovement)
        {
            cameraPosition.x -= cameraMovementSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cameraPosition.y -= scroll * scrollSpeed * Time.deltaTime;

        return cameraPosition;
    }

    private void SetNewCameraPosition(Vector3 cameraPosition)
    {
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -cameraMovementLimit.x + cameraCenterOffset, cameraMovementLimit.x + cameraCenterOffset);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, scrollBottomLimit, scrollUpperLimit);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, -cameraMovementLimit.y, cameraMovementLimit.y);

        transform.position = cameraPosition;
    }
}
