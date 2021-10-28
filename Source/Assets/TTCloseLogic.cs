using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTCloseLogic : MonoBehaviour
{
    void OnMouseEnter() {
        transform.localScale /= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Close", gameObject.transform.position);
    }
    void OnMouseExit() {
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }
    void OnMouseDown() {
        GameObject[] currentCells = GameObject.FindGameObjectsWithTag("truthtablecomponent");
            Debug.Log("Length of cells CURRENT IS " + currentCells.Length);
        foreach (GameObject obj in currentCells) {
            Debug.Log("Destroying " + obj);
            Destroy(obj);
        }
        GameObject.Find("Truth Table Canvas").SetActive(false);
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }
}
