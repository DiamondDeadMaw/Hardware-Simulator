using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Handler : MonoBehaviour
{
    public GameObject currentOutputConnection;  //the circle you are creating the line from, will be starting inputs, or output from a chip
    public bool DrawingLine = false;
    public bool foundInput = false;
    public bool dragging = false;
    public GameObject lineObject;
    GameObject newLine;
    Vector2 lineEndPoint;
    public GameObject currLine;
    public bool inSocket = false;
    public void drawLine(Vector3 startingPos, Vector2 mousePos, GameObject callingObj) {  //draws a line between two points, but only if the current line doesnt exist(connection hasnt been made)
        DrawingLine = true;
        currentOutputConnection = callingObj;
        //tries to get the script component of the calling object (a starting input, or a chip output)
        starting_Input_Script starting_Input_Obj;   
        Output outputScript;
        try {
            starting_Input_Obj = callingObj.GetComponent<starting_Input_Script>();
            currLine = starting_Input_Obj.currentLine;
        } catch {
            outputScript = callingObj.GetComponent<Output>();
            currLine = outputScript.currentLine;
        }
        if (currLine == null) {
            newLine = Instantiate(lineObject,startingPos, Quaternion.identity);
            newLine.GetComponent<Line_Logic>().startingObject = callingObj;
            try{
                callingObj.GetComponent<starting_Input_Script>().currentLine = newLine;
            } catch {
                callingObj.GetComponent<Output>().currentLine = newLine;
                callingObj.GetComponent<Output>().connectedLines.Add(newLine);
            }
        } else {
            newLine = currLine;
        }
        Vector3[] positions = new Vector3[2] {startingPos, new Vector3(mousePos.x, mousePos.y, 0)};

        if (inSocket) {
            currLine = null;
        }

        LineRenderer l_renderer = newLine.GetComponent<LineRenderer>();
        l_renderer.startColor = new Color(0,0,0,1);
        l_renderer.endColor = new Color(0,0,0,1);
        l_renderer.startWidth = 0.1f;
        l_renderer.endWidth = 0.1f;
        l_renderer.sortingLayerName= "Foreground";
        l_renderer.SetPositions(positions);
    }

    public void addNewOutputConnection(GameObject toAdd) {  //takes in the gameobject which is a socket
            try {
                currentOutputConnection.GetComponent<starting_Input_Script>().outputConnections.Add(toAdd);
                DrawingLine = false;
                currentOutputConnection.GetComponent<starting_Input_Script>().updateStates();
            } catch {
                currentOutputConnection.GetComponent<Output>().outputConnections.Add(toAdd);
                DrawingLine = false;
                currentOutputConnection.GetComponent<Output>().updateStates();
            }
            newLine.GetComponent<Line_Logic>().endingObject = toAdd;
            toAdd.GetComponent<InputSocket>().connectedLines.Add(newLine);
            Line_Logic log = newLine.GetComponent<Line_Logic>();
            newLine.GetComponent<LineRenderer>().SetPositions(new Vector3[] {log.startingObject.transform.position, log.endingObject.transform.position});
    }

    public void destroyCurrent() {
        //System.Threading.Thread.Sleep(250);
        if (currLine != null && (currLine.GetComponent<Line_Logic>().endingObject == null)) {
            Destroy(currLine);
            currLine = null;
        }
    }

    public void setCurrentLineNull() {
        currLine = null;
    }
    

}
