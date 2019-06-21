using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public IUserInput pi;
    public float horizontalSpeed = 80.0f;
    public float verticalSpeed = 60.0f;
    public float cameraDampValue = 0.5f;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject cam;

    private Vector3 cameraDampVelocity;

	void Awake () {

        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        model = playerHandle.GetComponent<ActorController>().model;
        cam = Camera.main.gameObject;
        tempEulerX = 20.0f;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void FixedUpdate () {

        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -40.0f, 30.0f);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0.0f, 0.0f);

        model.transform.eulerAngles = tempModelEuler;

        //cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position, 0.2f);
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        //cam.transform.eulerAngles = transform.eulerAngles;
        cam.transform.LookAt(cameraHandle.transform);
	}
}
