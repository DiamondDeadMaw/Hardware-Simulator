using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class Chip: MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject outputButtonPrefab;
    public List<GameObject> inputs;
    public List<GameObject> outputs;
    public List<GameObject> children = new List<GameObject>();
    public int numInputs;
    public int numOutputs;
    public string chipName;
    public GameObject nameText;
    void Start()
    {

        
    }


    void instantiateInputSocket(GameObject input) {
        inputs.Add(input);
        InputSocket scriptAccess = input.GetComponent<InputSocket>();
        scriptAccess.state = false;
        scriptAccess.parent = gameObject;
        children.Add(input);
    }

    void instantiateOutputSocket(GameObject output) {
        outputs.Add(output);
        Output scriptAccess = output.GetComponent<Output>();
        scriptAccess.state = false;
        scriptAccess.parent = gameObject;
        children.Add(output);
    }
    public void eval() {
        string inputState = "";
        List<GameObject> sortedInputs = inputs.OrderByDescending(o=>o.transform.position.y).ToList();  //since topmost inputs will have higher y positions
        foreach (GameObject input in sortedInputs) {
            if (input.GetComponent<InputSocket>().state) {
                inputState+="1";
            } else {
                inputState+="0";
            }
           
        }
        
        string outputState = GameObject.Find("GameHandler").GetComponent<Load_States>().getOutputState(chipName, inputState);
        List<GameObject> sortedOutputs = outputs.OrderByDescending(o=>o.transform.position.y).ToList();
        for (int i = 0; i<outputState.Length; i++) {
            if (outputState[i].Equals('1')) {
                sortedOutputs[i].GetComponent<Output>().updateState(true);
            } else {
                sortedOutputs[i].GetComponent<Output>().updateState(false);
            }
        }
    }
    
    public void create(string name, int inputNumber, int outputNumber, List<string> inputNames, List<string> outputNames) {
        chipName = name;
        numInputs =inputNumber;
        numOutputs = outputNumber;
        float width = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        GameObject text = (GameObject)Instantiate(nameText, transform.position + new Vector3(0.04899999f, 0.082f, 0), Quaternion.identity);
        children.Add(text);
        text.GetComponent<TextMeshPro>().SetText(name);

        float startX = gameObject.transform.position.x - width/2;
        float centreY = gameObject.transform.position.y;

        float radius = 0.35F;
        float spaceBW = radius/15;

        GameObject currentInputCircle;
        if (numInputs%2==0) {
            for (int i =numInputs/2 -1; i>=0; i--) {
                currentInputCircle = (GameObject)Instantiate(buttonPrefab, new Vector3(startX, centreY + spaceBW/2 + radius + (2*radius + spaceBW)*i,0), Quaternion.identity);
                instantiateInputSocket(currentInputCircle);
            }
            for (int i = 0; i<numInputs/2;i++) {
                currentInputCircle = (GameObject)Instantiate(buttonPrefab, new Vector3(startX, centreY - spaceBW/2 - radius - (2*radius + spaceBW)*i, 0),Quaternion.identity);
                instantiateInputSocket(currentInputCircle);
            }
            float newHeight = spaceBW + 2*radius*numInputs + spaceBW*(numInputs-1);
            this.transform.localScale = new Vector3(width,newHeight,0);
        } else if (numInputs%2!=0) {
            for (int i = numInputs/2 -1; i>=0; i--) {
                currentInputCircle = (GameObject)Instantiate(buttonPrefab, new Vector3(startX, centreY + 2*radius + spaceBW + (2*radius+spaceBW)*i,0), Quaternion.identity);
                instantiateInputSocket(currentInputCircle);
            }
            currentInputCircle = (GameObject)Instantiate(buttonPrefab, new Vector3(startX, centreY, 0), Quaternion.identity);
            instantiateInputSocket(currentInputCircle);
            for (int i =0; i<numInputs/2; i++) {
                currentInputCircle = (GameObject)Instantiate(buttonPrefab, new Vector3(startX, centreY - 2*radius - spaceBW - (2*radius+spaceBW)*i, 0), Quaternion.identity);
                instantiateInputSocket(currentInputCircle);
            }
            float newHeight = spaceBW + 2*radius*numInputs + spaceBW*(numInputs-1);
            gameObject.transform.localScale = new Vector3(width, newHeight, 0);
        }

        //rendering outputs
        GameObject currentOutputCircle;
        float startXOutput = gameObject.transform.position.x + width/2;
        if (numOutputs%2==0) {
            for (int i =numOutputs/2 -1; i>=0; i--) {
                currentOutputCircle = (GameObject)Instantiate(outputButtonPrefab, new Vector3(startXOutput, centreY + spaceBW/2 + radius + (2*radius + spaceBW)*i,0), Quaternion.identity);
                instantiateOutputSocket(currentOutputCircle);
            }
            for (int i = 0; i<numOutputs/2;i++) {
                currentOutputCircle = (GameObject)Instantiate(outputButtonPrefab, new Vector3(startXOutput, centreY - spaceBW/2 - radius - (2*radius + spaceBW)*i, 0),Quaternion.identity);
                instantiateOutputSocket(currentOutputCircle);
            }
        } else {
            for (int i = numOutputs/2 -1; i>=0; i--) {
                currentOutputCircle = (GameObject)Instantiate(outputButtonPrefab, new Vector3(startXOutput, centreY + 2*radius + spaceBW + (2*radius+spaceBW)*i,0), Quaternion.identity);
                instantiateOutputSocket(currentOutputCircle);
            }
            currentOutputCircle = (GameObject)Instantiate(outputButtonPrefab, new Vector3(startXOutput, centreY, 0), Quaternion.identity);
            instantiateOutputSocket(currentOutputCircle);
            for (int i =0; i<numOutputs/2; i++) {
                currentOutputCircle = (GameObject)Instantiate(outputButtonPrefab, new Vector3(startXOutput, centreY - 2*radius - spaceBW - (2*radius+spaceBW)*i, 0), Quaternion.identity);
                instantiateOutputSocket(currentOutputCircle);
            }
        }

        List<GameObject> sortedInputs = inputs.OrderByDescending(o=>o.transform.position.y).ToList();
        for (int i = 0; i<inputNames.Count; i++) {
            sortedInputs[i].GetComponent<InputSocket>().setName(inputNames[i]);
            Debug.Log("setting name to " + inputNames[i]);
            }
        
        List<GameObject> sortedOutputs = outputs.OrderByDescending(o=>o.transform.position.y).ToList();
        for (int i = 0; i<outputNames.Count; i++) {
            sortedOutputs[i].GetComponent<Output>().setName(outputNames[i]);
        }
    }
}
