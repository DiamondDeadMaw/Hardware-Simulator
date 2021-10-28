using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class chipDrag : MonoBehaviour, IDragHandler
{
    List<GameObject> children;
    void Start()
    {
        children = gameObject.GetComponent<Chip>().children;
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
