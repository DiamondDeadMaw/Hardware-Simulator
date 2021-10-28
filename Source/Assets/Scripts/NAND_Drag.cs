using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NAND_Drag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{   
    public GameObject input1;
    public GameObject input2;
    public GameObject nameText;
    public GameObject output;
    List<GameObject> children;

    void Start() {
        children = new List<GameObject>();
        Vector3 pos = gameObject.transform.position;
        children.Add(Instantiate(input1, pos + new Vector3(-1.976f, 0.51f, 0), Quaternion.identity));
        children.Add(Instantiate(input2, pos + new Vector3(-1.976f, -0.51f, 0), Quaternion.identity));
        children.Add(Instantiate(output, pos + new Vector3(2f,0, 0), Quaternion.identity));
        children.Add(Instantiate(nameText, pos + new Vector3(0.04899999f, 0.082f, 0), Quaternion.identity));
        

        children[0].GetComponent<InputSocket>().parent = gameObject;
        children[1].GetComponent<InputSocket>().parent = gameObject;
        children[2].GetComponent<Output>().parent = gameObject;
        gameObject.GetComponent<NAND_Logic>().inputs = new InputSocket[2] {children[0].GetComponent<InputSocket>(), children[1].GetComponent<InputSocket>()};
        gameObject.GetComponent<NAND_Logic>().output = children[2].GetComponent<Output>();
    }
    public void OnBeginDrag(PointerEventData eventData) {
    }

    public void OnEndDrag(PointerEventData eventData) {
    } 

    public void OnPointerDown(PointerEventData eventData) {
    }

    public void OnDrag(PointerEventData eventData) {
       float h = Screen.height;  //scales delta up and down depending on resolution
       float w = Screen.width;
       float scale2 = h/w;
       float scale = Mathf.Max(Screen.width, Screen.height) * scale2/21;                 //weird magic number
       Vector3 delta = new Vector3(eventData.delta.x/scale, eventData.delta.y/scale,0);
       transform.position += delta;
       moveChildren(delta);
    }

    void moveChildren(Vector3 delt) {
        foreach(GameObject child in children) {
            child.transform.position +=delt;
            try {
                child.GetComponent<InputSocket>().updateLines();
            } catch {
            }
            try {
                child.GetComponent<Output>().updateLines();
            } catch {

            }
        }
    }
}
