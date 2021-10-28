using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundZoom : MonoBehaviour
{   
    Camera cam;
    CameraController camController;
    float scrollDelta;
    void Start() {
        cam = Camera.main;
        camController = GameObject.Find("GameHandler").GetComponent<CameraController>();
    }

    void Update() {
        scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta!=0f) {
            cam.orthographicSize+=scrollDelta;
            camController.updatePositions(scrollDelta);
        }
    }
}
