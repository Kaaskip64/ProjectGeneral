using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VcamSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera FreelookTopdown;
    public CinemachineCameraOffset topdownOffset;
    public CinemachineVirtualCamera FollowplayerTopdown;
    public CinemachineVirtualCamera FollowplayerBack;
    public CinemachineVirtualCamera FirstPersonCamPos;
    public Camera FPScam;
    public MeshRenderer playerMesh;

    public CameraController cameraController;
    public FPSmode CamFPS;

    private FirstPersonController FPS;

    public KeyCode cameraSwitchButton;

    public int camInterval;

    private bool camSwitch = false;

    private void Start()
    {
        FPS = FirstPersonController.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(cameraSwitchButton))
        {
            camSwitch = !camSwitch;
            if(camSwitch)
            {
                print("topdown to fps");
                StartCoroutine(MoveToFPS());
            }
            if(!camSwitch)
            {
                print("fps to topdown");
                StartCoroutine(FPSToTopdown());
            }
        }
    }

    public IEnumerator MoveToFPS()
    {
        cameraController.SetActive();

        KeyCode temp = cameraSwitchButton;
        cameraSwitchButton = KeyCode.End;

        FreelookTopdown.enabled = false;
        yield return new WaitForSeconds(camInterval);
        FollowplayerTopdown.enabled = false;
        yield return new WaitForSeconds(camInterval);
        FollowplayerBack.enabled = false;
        yield return new WaitForSeconds(camInterval);

        cameraSwitchButton = temp;

        playerMesh.enabled = false;
        FPScam.enabled = true;
        FPS.SwitchFPSmode();
        CamFPS.FirstPersonCamOn = true;
        Cursor.lockState = CursorLockMode.Locked;


        FirstPersonCamPos.enabled = false;

    }

    public IEnumerator FPSToTopdown()
    {
        FPS.rb.velocity = new Vector3(0, 0, 0);
        FollowplayerBack.enabled = false;
        FPScam.enabled = false;
        FPS.SwitchFPSmode();
        CamFPS.FirstPersonCamOn = false;
        playerMesh.enabled = true;
        FirstPersonCamPos.enabled = true;

        Cursor.lockState = CursorLockMode.Confined;


        KeyCode temp = cameraSwitchButton;
        cameraSwitchButton = KeyCode.End;

        yield return new WaitForSeconds(camInterval);
        FollowplayerBack.enabled = true;

        yield return new WaitForSeconds(camInterval);
        FollowplayerTopdown.enabled = true;
        topdownOffset.m_Offset.z = 0;
        yield return new WaitForSeconds(camInterval);
        cameraController.SetActive();
        cameraSwitchButton = temp;
        FreelookTopdown.transform.position = FollowplayerTopdown.transform.position;
        FreelookTopdown.enabled = true;
    }
}
