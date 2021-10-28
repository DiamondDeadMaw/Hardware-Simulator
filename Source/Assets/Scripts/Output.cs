using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Output : MonoBehaviour, IDragHandler
{
    // Start is called before the first frame update
    public bool state = false;
    public string outputName = "";
    SpriteRenderer sp_renderer;

    public GameObject parent;

    public GameObject currentLine;
    public List<GameObject> connectedLines;

    public List<GameObject> outputConnections;
    void Start()
    {
        sp_renderer = gameObject.GetComponent<SpriteRenderer>();
        currentLine = null;
        connectedLines = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateState(bool newState) {
        state = newState;
        if (state) {
            sp_renderer.color = new Color(200,0,0,1);
        } else {
            sp_renderer.color = new Color(255,255,255,1);
        }
        foreach (GameObject connection in outputConnections) {
            try {
                InputSocket sock = connection.GetComponent<InputSocket>();
                sock.updateState(state);
            } catch {
                starting_Output_Script finalOut = connection.GetComponent<starting_Output_Script>();
                finalOut.updateState(state);
            }
            
        }
    }

    float scaleChange = 0.05f;
    void OnMouseEnter() {
        transform.localScale += new Vector3(scaleChange, scaleChange,scaleChange);
        if (outputName.Length!=0) {
            GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip(outputName, gameObject.transform.position);
        } 
    }
    void OnMouseExit() {
        transform.localScale -= new Vector3(scaleChange, scaleChange, scaleChange);
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }

    public void OnDrag(PointerEventData eventData) {
        Line_Handler lh = GameObject.Find("GameHandler").GetComponent<Line_Handler>();
        lh.currentOutputConnection = gameObject;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lh.drawLine(transform.position, mousePos, gameObject);
    }

    public void updateLines() {
        foreach(GameObject line in connectedLines) {
            line.GetComponent<Line_Logic>().updateLineEnds();
        }
    }

    public void updateStates() {
        foreach (GameObject output in outputConnections) {
            try {
                output.GetComponent<InputSocket>().updateState(this.state);
            } catch {
                output.GetComponent<starting_Output_Script>().updateState(this.state);
            }
            
        }
    }
    public void setName(string newName) {
        outputName = newName;
    }

    public string getName() {
        return outputName;
    }
}
