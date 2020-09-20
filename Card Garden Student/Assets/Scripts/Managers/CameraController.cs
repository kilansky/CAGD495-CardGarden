using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // key to reset to start point (space)

    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float xPanLimit, xNegPanLimit, zPanLimit, zNegPanlimit = 20f;
    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 120f;

    private Vector3 resetLocation;
    private Quaternion resetRotation;
    private Vector3 resetScale;


    private void Awake()
    {
        resetLocation = transform.position;
        resetRotation = transform.rotation;
        resetScale = transform.localScale;

    }

    private void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, xNegPanLimit, xPanLimit);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, zNegPanlimit, zPanLimit);
        transform.position = pos;

        if (Input.GetKey(KeyCode.Space))
        {
            ResetCamera();
        }
    }


    public void ResetCamera()
    {
        transform.position = resetLocation;
        transform.rotation = resetRotation;
        transform.localScale = resetScale;
    }
}
