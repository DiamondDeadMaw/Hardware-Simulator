using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class starting_Output_Script : MonoBehaviour
{
    bool state = false;
    bool flag = false;
    string outputName;

    public Line_Handler lhandler;
    public List<GameObject> connectedLines;
    SpriteRenderer sp_renderer;
    public GameObject inputFieldPrefab;
    GameObject canvas;
    void Start() {
        lhandler = GameObject.Find("GameHandler").GetComponent<Line_Handler>();
        sp_renderer = GetComponent<SpriteRenderer>();
        connectedLines = new List<GameObject>();
        outputName = "";
    }
    void OnMouseEnter() {
        transform.localScale /= 0.9f;
        if (lhandler.DrawingLine && Input.GetMouseButton(0)) {
            flag = true;
        }
        if (outputName.Length==0) {
            GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Right Click to Rename Output", gameObject.transform.position);
        } else {
            GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip(outputName, gameObject.transform.position);
        }
    }
    void OnMouseExit() {
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }
    void Update() {
        bool mouseIn = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 inputPos = transform.position;
        Vector2 boundsSize = GetComponent<SpriteRenderer>().bounds.size;
        if ((mousePos.x> (inputPos.x - boundsSize.x))&& (mousePos.x < (inputPos.x + boundsSize.x)) && (mousePos.y<(inputPos.y +boundsSize.y)) && (mousePos.y > (inputPos.y-boundsSize.y))) {
            mouseIn = true;
        }
        if (flag && Input.GetMouseButtonUp(0) && mouseIn) {           //the mouse has stopped dragging on this input slot
            lhandler.DrawingLine = false;
            lhandler.addNewOutputConnection(gameObject);
            flag = false;
            foreach (GameObject line in connectedLines) {
                try {
                     line.GetComponent<Line_Logic>().startingObject.GetComponent<starting_Input_Script>().currentLine= null;
                } catch {
                    line.GetComponent<Line_Logic>().startingObject.GetComponent<Output>().currentLine = null;
                }
               
            }
        }
    }



    void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            canvas = Instantiate(inputFieldPrefab, Camera.main.transform.position, Quaternion.identity);
            canvas.GetComponent<Canvas>().worldCamera = Camera.main;
            Text text = GameObject.Find("Placeholder").GetComponent<Text>();
            text.text = "Enter new output name";
            InputField ifield = canvas.GetComponentInChildren<InputField>();
            ifield.onEndEdit.AddListener(delegate{acceptInput(ifield);});     
        }
    }
    void acceptInput(InputField itext) {
        outputName = itext.text;
        Destroy(canvas);
    }

    public void updateState(bool newState) {
        state = newState;
        if (state) {
            sp_renderer.color = new Color(200,0,0,1);
        } else {
            sp_renderer.color = new Color(255,255,255,1);
        }
    }

    public string getState() {
        if (state) {
            return "1";
        } else {
            return "0";
        }
    }
    public void setName(string newName) {
        outputName = newName;
    }

    public string getName() {
        return outputName;
    }
}
