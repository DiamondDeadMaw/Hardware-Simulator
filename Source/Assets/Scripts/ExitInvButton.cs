using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitInvButton : MonoBehaviour
{
    void OnMouseEnter() {
        transform.localScale /= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Close Inventory", gameObject.transform.position);
    }
    void OnMouseExit() {
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }

    void OnMouseDown() {
        GameObject.Find("GameHandler").GetComponent<InventoryController>().deActivateInventory();
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }
}
