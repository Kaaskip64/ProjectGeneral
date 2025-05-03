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
    public GameObject FPScam;
    public MeshRenderer playerMesh;

    public CameraController cameraController;

    private FirstPersonController FPS;
    private FPSmode CamFPS;

    public KeyCode cameraSwitchButton;

    public int camInterval;

    private bool camSwitch = false;

    private void Start()
    {
        FPS = FirstPersonController.instance;
        CamFPS = FPSmode.instance;
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

        FreelookTopdown.gameObject.SetActive(false);
        yield return new WaitForSeconds(camInterval);
        FollowplayerTopdown.gameObject.SetActive(false);
        yield return new WaitForSeconds(camInterval);
        FirstPersonCamPos.gameObject.SetActive(true);
        yield return new WaitForSeconds(camInterval);
        cameraSwitchButton = temp;

        playerMesh.enabled = false;
        FPScam.SetActive(true);
        FPS.SwitchFPSmode();
        CamFPS.FirstPersonCamOn = true;
        Cursor.lockState = CursorLockMode.Locked;


        FirstPersonCamPos.gameObject.SetActive(false);

        FollowplayerBack.gameObject.SetActive(false);
    }

    public IEnumerator FPSToTopdown()
    {
        FollowplayerBack.gameObject.SetActive(true);
        FirstPersonCamPos.gameObject.SetActive(false);
        FPS.SwitchFPSmode();
        FPScam.SetActive(false);
        CamFPS.FirstPersonCamOn = false;
        playerMesh.enabled = true;

        Cursor.lockState = CursorLockMode.Confined;


        FirstPersonCamPos.gameObject.SetActive(true);


        KeyCode temp = cameraSwitchButton;
        cameraSwitchButton = KeyCode.End;

        yield return new WaitForSeconds(camInterval);
        FollowplayerTopdown.gameObject.SetActive(true);
        FreelookTopdown.transform.position = FollowplayerTopdown.transform.position;
        topdownOffset.m_Offset.z = 0;
        yield return new WaitForSeconds(camInterval);
        cameraController.SetActive();
        cameraSwitchButton = temp;
        FreelookTopdown.gameObject.SetActive(true);
    }
}
