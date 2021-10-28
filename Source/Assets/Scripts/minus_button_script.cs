using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minus_button_script : MonoBehaviour
{
    void Start() {
        Camera cam = Camera.main;
        float size = cam.orthographicSize;
        float ratio = cam.aspect;
        float diff = 1.786f;
        transform.position = cam.transform.position - new Vector3(size*ratio - diff, 9.404f, -10);
    }
    void OnMouseEnter() {
        transform.localScale /= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Remove Input", gameObject.transform.position);
    }
    void OnMouseExit() {
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }

    void OnMouseDown() {
        starting_inputs_handler handler = GameObject.Find("GameHandler").GetComponent<starting_inputs_handler>();
        handler.addNewInputButton(true);
    }
}
