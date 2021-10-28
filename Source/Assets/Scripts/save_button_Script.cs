using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_button_Script : MonoBehaviour
{
    void OnMouseEnter() {
        transform.localScale /= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Create New Chip", gameObject.transform.position);
    }
    void OnMouseExit() {
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }
    void OnMouseDown() {
        save_States handler = GameObject.Find("GameHandler").GetComponent<save_States>();
        handler.saveStates();
    }

}
