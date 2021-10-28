using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToolTipController : MonoBehaviour
{
    public GameObject toolTip;
    TextMeshPro textObj;
    Camera cam;
    void Start()
    {
        textObj = toolTip.GetComponentInChildren<TextMeshPro>();
        toolTip.SetActive(false);
        cam = Camera.main;
    }

    public void newToolTip(string text, Vector3 pos) {
        Debug.Log("Position: " + pos + " Cam pos: " + (cam.transform.position.x + cam.orthographicSize*cam.aspect/2));
        toolTip.SetActive(true);
        toolTip.transform.position = pos + new Vector3(0,1.6f,0);
        if (toolTip.transform.position.y >= cam.orthographicSize-2) {
            toolTip.transform.position -= new Vector3(0,3.2f,0);
        }
        if (pos.x <= (cam.transform.position.x - cam.orthographicSize*cam.aspect +2)) {
            toolTip.transform.position += new Vector3(5f,-1.6f,0);
        }
        if (pos.x >= (cam.transform.position.x + cam.orthographicSize*cam.aspect - 2)) {
            toolTip.transform.position -= new Vector3(5f,1.6f,0);
        }
        textObj.text = text;
        
    }

    public void destroyToolTip() {
        toolTip.SetActive(false);
    }

    void Update() {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (toolTip.activeSelf) {
            //toolTip.transform.position = mousePos;
        }
    }
}
