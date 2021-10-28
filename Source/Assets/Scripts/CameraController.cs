using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject[] bottomItems;
    public GameObject inventoryButton;
    public GameObject inputAdd;
    public GameObject inputRemove;
    public GameObject outputAdd;
    public GameObject outputRemove;
    public GameObject saveButton;

    GameObject[] topItems;
    public GameObject restartButton;
    public GameObject TTButton;

    GameObject[] leftItems;
    GameObject[] rightItems;
    float aspect;
    void Start() {
        bottomItems = new GameObject[] {inventoryButton, inputAdd, inputRemove, outputAdd, outputRemove, saveButton};
        topItems = new GameObject[] {restartButton, TTButton};
        aspect = Camera.main.aspect;
    }
    public void updateElements() {
        leftItems = GameObject.FindGameObjectsWithTag("starting_input");
        rightItems = GameObject.FindGameObjectsWithTag("starting_ouput");
    }

    public void updatePositions(float delt) {
        foreach(GameObject obj in bottomItems) {
            obj.transform.position -= new Vector3(0,delt, 0);
        }
        foreach(GameObject obj in topItems) {
            obj.transform.position += new Vector3(0,delt, 0);
        }
        foreach(GameObject obj in leftItems) {
            obj.transform.position -= new Vector3(delt * aspect,0, 0);
        }
        foreach(GameObject obj in rightItems) {
            obj.transform.position += new Vector3(delt * aspect,0, 0);
        }
    }
}
