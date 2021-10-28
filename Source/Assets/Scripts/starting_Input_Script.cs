using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class starting_Input_Script : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public List<GameObject> outputConnections;
    bool state = false;

    bool mouseDown = false;
    public GameObject currentLine;
    string inputName;
    public GameObject inputFieldPrefab;
    GameObject canvas;
    void Start()
    {
        currentLine = null;
        outputConnections = new List<GameObject>();
        inputName = "";
    }

    // Update is called once per frame
    void OnMouseEnter() {
        transform.localScale /= 0.9f;
        if (inputName.Length==0) {
            GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Right Click to Rename Input", gameObject.transform.position);
        } else {
            GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip(inputName, gameObject.transform.position);
        }

    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            canvas = Instantiate(inputFieldPrefab, Camera.main.transform.position, Quaternion.identity);
            canvas.GetComponent<Canvas>().worldCamera = Camera.main;
            Text text = GameObject.Find("Placeholder").GetComponent<Text>();
            text.text = "Enter new input name";
            InputField ifield = canvas.GetComponentInChildren<InputField>();
            ifield.onEndEdit.AddListener(delegate{acceptInput(ifield);});     
        }
    }

    void acceptInput(InputField itext) {
        inputName = itext.text;
        Destroy(canvas);
    }



    void OnMouseExit() {
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }
    
    void OnMouseDown() {
        mouseDown = true;
    }

    void OnMouseUp() {
        bool mouseIn = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 inputPos = transform.position;
        Vector2 boundsSize = GetComponent<SpriteRenderer>().bounds.size;
        if ((mousePos.x> (inputPos.x - boundsSize.x))&& (mousePos.x < (inputPos.x + boundsSize.x)) && (mousePos.y<(inputPos.y +boundsSize.y)) && (mousePos.y > (inputPos.y-boundsSize.y))) {
            mouseIn = true;
        }
        if (mouseDown && mouseIn) {
            state = !state;
        updateStates();                //for when user clicks on input to change its state
        SpriteRenderer s_renderer = gameObject.GetComponent<SpriteRenderer>();
        if (state) {
            s_renderer.color = new Color(255,0,0,1);
        } else {
            s_renderer.color = new Color(255,255,255,1);
        }
        }
        mouseDown = false;
    }
    public void OnDrag(PointerEventData eventData) {
        GameObject.Find("GameHandler").GetComponent<Line_Handler>().currentOutputConnection = gameObject;    //gets position of mouse in world coords, and sends them over to linerender
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject.Find("GameHandler").GetComponent<Line_Handler>().drawLine(transform.position, mousePos, gameObject);
        GameObject.Find("GameHandler").GetComponent<Line_Handler>().dragging = true;

    }

    public void OnEndDrag(PointerEventData eventData) {
        GameObject.Find("GameHandler").GetComponent<Line_Handler>().dragging = false; //SHOULD DESTROY IF CONNECTION NOT MADE, TO FIX
        GameObject.Find("GameHandler").GetComponent<Line_Handler>().destroyCurrent();
    }

    public void updateStates() {
        foreach (GameObject socket in outputConnections) {
            socket.GetComponent<InputSocket>().updateState(this.state);
        }
    }

    public void setState(int newState) {
        if (newState ==1) {
            state = true;
        } else {
            state = false;
        }
        updateStates();
    }

    public void setName(string newName) {
        inputName = newName;
    }

    public string getName() {
        return inputName;
    }
}
