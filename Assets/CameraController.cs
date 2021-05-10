using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float minZoom = 30f;
    public float maxZoom = 60f;
    public float zoomSpeed = 15f;

    private float currentZoom = 60f;

    void Update() {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        Debug.Log(currentZoom);
    }

    void LateUpdate() {
        Camera.main.fieldOfView = currentZoom;
    }
}