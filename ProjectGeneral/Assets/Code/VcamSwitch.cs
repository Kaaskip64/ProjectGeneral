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
    public CinemachineVirtualCamera FirstPersonCam;

    public CameraController cameraController;

    public KeyCode cameraSwitchButton;

    public int camInterval;

    private bool camSwitch = false;

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
        cameraSwitchButton = temp;
        FollowplayerBack.gameObject.SetActive(false);
    }

    public IEnumerator FPSToTopdown()
    {
        FollowplayerBack.gameObject.SetActive(true);

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
