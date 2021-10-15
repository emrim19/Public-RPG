using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private Vector3 offset;

    private float currentZoom = 3f;
    private float zoomSpeed = 4f;
    private float minZoom = 2;
    private float maxZoom = 5;

    private float pitch = 2;

    private float yawSpeed = 100f;
    private float currentYaw = 0f;

    private float currentHeight = 0;


    // Start is called before the first frame update
    void Start() {
        target = GameObject.Find("Player").GetComponent<Transform>();

        offset = new Vector3(0, -3, 3);
    }

    // Update is called once per frame
    void Update() {
        Zoom();
    }

    void LateUpdate() {
        FollowPlayer();
        RotateCamera();
    }


    public void FollowPlayer() {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
    }

    public void Zoom() {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        currentHeight += Input.GetAxis("Vertical") * yawSpeed * Time.deltaTime;
    }

    private void RotateCamera() {
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }

}
