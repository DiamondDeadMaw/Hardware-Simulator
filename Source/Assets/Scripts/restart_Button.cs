using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart_Button : MonoBehaviour
{
    GameObject[] objectsToDelete;
    void OnMouseEnter() {
        transform.localScale /= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Remove Chips on Board", gameObject.transform.position);
    }
    void OnMouseExit() {
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }


    /*
    void OnMouseDown() {
        objectsToDelete = GameObject.FindGameObjectsWithTag("gameobject");
        foreach (GameObject obj in objectsToDelete) {
            Destroy(obj);
        }
        GameObject handler = GameObject.Find("GameHandler");
        handler.GetComponent<starting_inputs_handler>().Start();
        handler.GetComponent<starting_outputs_handler>().Start();
    }
    */

    void OnMouseDown() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
