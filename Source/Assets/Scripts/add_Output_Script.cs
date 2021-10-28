using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_Output_Script : MonoBehaviour
{
    void OnMouseEnter() {
        transform.localScale /= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Add New Output", gameObject.transform.position);
    }
    void OnMouseExit() {
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }

    void OnMouseDown() {
        starting_outputs_handler handler = GameObject.Find("GameHandler").GetComponent<starting_outputs_handler>();
        handler.addNewOutputButton(false);
    }
}
