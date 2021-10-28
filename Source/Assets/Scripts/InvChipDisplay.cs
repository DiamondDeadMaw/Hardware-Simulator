using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
public class InvChipDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{   
    public string chip_name;
    public GameObject nandChip;
    bool mouseIn = false;
    public void setText(string name) {
        chip_name = name;
        TextMeshProUGUI textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textMesh.SetText(name); 
        if (name.Length>=6) {
            textMesh.fontSize = 0.45f;
        }
    }


    public void OnPointerClick(PointerEventData eventData) {
        try {
            GameObject.Find("GameHandler").GetComponent<makeChip>().createChip(chip_name);
        } catch (ArgumentOutOfRangeException e) {
            Debug.Log(e);
        } catch {
            Instantiate(nandChip, Camera.main.transform.position + new Vector3(0,0,21), Quaternion.identity);
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouseIn = true;
        transform.localScale /= 0.9f;
        if (chip_name.Length !=0) {
            GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Right Click to Delete", gameObject.transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseIn = false;
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }

    void Update() {
        if (mouseIn && Input.GetMouseButtonDown(1) && chip_name.Length>0) {
            GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
            Destroy(gameObject);
            GameObject.Find("GameHandler").GetComponent<Load_States>().deleteFile(chip_name);
        }
    }


}
