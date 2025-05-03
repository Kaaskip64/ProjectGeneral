using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSmode : MonoBehaviour
{
	public static FPSmode instance;

	public float mouseSensitivity = 100f;
	public Transform playerBody;
	public Camera camFPS;
	float X_Rotation = 0f;

	public bool FirstPersonCamOn = false;

    private void Start()
    {
		instance = this;
    }

    void Update()
	{
		if (FirstPersonCamOn)
        {
			CamMove();
        } else
        {
			return;
        }
	}

	public void CamMove()
    {
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		X_Rotation -= mouseY;
		X_Rotation = Mathf.Clamp(X_Rotation, -89f, 89f);

		camFPS.transform.localRotation = Quaternion.Euler(X_Rotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * mouseX);
	}

}
