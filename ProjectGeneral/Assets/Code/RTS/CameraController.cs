using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Panning")]
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;

    [Space(20)]

    [Header("Zoom Boundaries (dont make min bigger than max)")]
    [Range(-225, 10)]
    public int minZoom = -100;
    [Range(-225, 10)]
    public int maxZoom = 10;

    [Space(30)]
    [Header("Misc Zoom Variables")]
    public float zoomSpeed = 10;
    public float zoomSensitivity = 5f;

    [Header("Camera State")]
    public bool cameraActive;
    [Header("Virtual Camera")]
    public CinemachineVirtualCamera vcam;
    public CinemachineCameraOffset vcamOffset;

    private float zoomTarget;

    public static CameraController instance;

    void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Confined;
        zoomTarget = vcam.m_Lens.FieldOfView;

    }

    void Update()
    {
        if (minZoom > maxZoom)
        {
            print("Keep min cam zoom below max cam zoom");
            Destroy(this);
        }

        if (cameraActive)
        {
            CameraMove();
        }

    }

    void CameraMove()
    {
        Vector3 pos = vcam.transform.position;
        if (Input.mousePosition.y >= Screen.height - panBorderThickness || Input.GetKey(KeyCode.W)) //up
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness || Input.GetKey(KeyCode.A)) //left
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness || Input.GetKey(KeyCode.S)) //down
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness || Input.GetKey(KeyCode.D)) //right
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        vcam.transform.position = pos;

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0)
        {
            float offset = scrollWheel * zoomSensitivity;
            float target = Mathf.Clamp(vcamOffset.m_Offset.z + offset, minZoom, maxZoom);

            vcamOffset.m_Offset.z = Mathf.Lerp(vcamOffset.m_Offset.z, target, zoomSensitivity * Time.deltaTime);
        }
    }

    public void SetActive()
    {
        cameraActive = !cameraActive;
    }


}

