using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InputSocket : MonoBehaviour
{
    public bool state = false;
    public string inputName = "";

    public GameObject parent;
    public GameObject Handler;
    Line_Handler lHandler;
    SpriteRenderer sp_renderer;

    public List<GameObject> connectedLines;

    bool flag = false;

    void Start()
    {
        sp_renderer = GetComponent<SpriteRenderer>();
        Handler = GameObject.Find("GameHandler");
        lHandler = Handler.GetComponent<Line_Handler>();
        connectedLines = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        bool mouseIn = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 inputPos = transform.position;
        Vector2 boundsSize = GetComponent<SpriteRenderer>().bounds.size;
        if ((mousePos.x> (inputPos.x - boundsSize.x))&& (mousePos.x < (inputPos.x + boundsSize.x)) && (mousePos.y<(inputPos.y +boundsSize.y)) && (mousePos.y > (inputPos.y-boundsSize.y))) {
            mouseIn = true;
        }
        if (flag && Input.GetMouseButtonUp(0) && mouseIn) {           //the mouse has stopped dragging on this input slot
            lHandler.DrawingLine = false;
            lHandler.addNewOutputConnection(gameObject);
            flag = false;
            foreach (GameObject line in connectedLines) {
                try {
                     line.GetComponent<Line_Logic>().startingObject.GetComponent<starting_Input_Script>().currentLine= null;
                } catch {
                    line.GetComponent<Line_Logic>().startingObject.GetComponent<Output>().currentLine = null;
                }
               
            }
        }
        if (mouseIn && Input.GetKey(KeyCode.D)) {
            foreach (GameObject line in connectedLines) {
                try {
                    line.GetComponent<Line_Logic>().startingObject.GetComponent<starting_Input_Script>().outputConnections.Remove(gameObject);
                } catch {
                    line.GetComponent<Line_Logic>().startingObject.GetComponent<Output>().outputConnections.Remove(gameObject);
                }
                
                Destroy(line);
            }
            connectedLines = new List<GameObject>();
        }
    }
    public void updateState(bool newState) {
        state = newState;
        if (state) {
            sp_renderer.color = new Color(200,0,0,1);
        } else {
            sp_renderer.color = new Color(255,255,255,1);
        }
        try {
            parent.GetComponent<NAND_Logic>().eval();
        } catch {
            parent.GetComponent<Chip>().eval();          
        }
    }

    float scaleChange = 0.05f;
    void OnMouseEnter() {
        transform.localScale += new Vector3(scaleChange, scaleChange,scaleChange);
        if (lHandler.DrawingLine && Input.GetMouseButton(0)) {
            flag = true;  
            lHandler.setCurrentLineNull();  
            lHandler.inSocket = true;    
        }
        if (inputName.Length!=0) {
            GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip(inputName, gameObject.transform.position);
        } 
    }
    void OnMouseExit() {
        transform.localScale -= new Vector3(scaleChange, scaleChange, scaleChange);
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }

    public void updateLines() {
        foreach (GameObject line in connectedLines) {
            line.GetComponent<Line_Logic>().updateLineEnds();
        }
    }
    public void setName(string newName) {
        inputName = newName;
    }

    public string getName() {
        return inputName;
    }



}
