using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GenTruthTable : MonoBehaviour
{

    List<string> inputPermutations;
    List<string> outputPermutations;
    GameObject table;
    public GameObject cell;
    int[] arr;

    bool mouseDown = false;
    
    void Start() {
        inputPermutations = new List<string>();
        outputPermutations = new List<string>();
        table = GameObject.Find("Truth Table Canvas");
        table.SetActive(false);
    }

    void getStateLists() {
        List<GameObject> inputs = GameObject.Find("GameHandler").GetComponent<starting_inputs_handler>().inputs;
        List<GameObject> outputs = GameObject.Find("GameHandler").GetComponent<starting_outputs_handler>().outputs;
        inputPermutations = new List<string>();
        outputPermutations = new List<string>();
        int numBits = inputs.Count;
        
        arr = new int[numBits];
        findPermutation(numBits, arr, 0);

        foreach (string state in inputPermutations) {
            for (int i = 0; i<numBits; i++) {
                    inputs[i].GetComponent<starting_Input_Script>().setState(int.Parse(state[i].ToString()));      //sets the state of each input to the corresponding bit
            }

            //after all inputs have been set to a permutation, we now read the value of the output
            string outputPerm = "";
            for (int i = 0; i<outputs.Count; i++) {
                outputPerm += outputs[i].GetComponent<starting_Output_Script>().getState();
            }
            outputPermutations.Add(outputPerm);
        }
    }
    void findPermutation(int n, int[] arr, int i) {
        if (i==n) {
            inputPermutations.Add(string.Join("",arr));
            return;
        }

        arr[i]= 0;
        findPermutation(n, arr, i+1);

        arr[i]=1;
        findPermutation(n, arr, i+1);
    }


    void OnMouseEnter() {
        transform.localScale /= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().newToolTip("Generate Truth Table", gameObject.transform.position);
    }
    void OnMouseExit() {
        transform.localScale *= 0.9f;
        GameObject.Find("GameHandler").GetComponent<ToolTipController>().destroyToolTip();
    }

    void OnMouseDown() {
        
        mouseDown = true;
        
    }

    void OnMouseUp() {
        if (mouseDown) {
            mouseDown = false;
            
        getStateLists();
        table.SetActive(true);
        
        int numInputBits = inputPermutations[0].Length;
        int numOutputBits = outputPermutations[0].Length;

        //destroying pre-existing cells




        List<GameObject> startingInputs = GameObject.Find("GameHandler").GetComponent<starting_inputs_handler>().inputs;
        List<string> names = new List<string>();
        foreach (GameObject cinput in startingInputs) {
            names.Add(cinput.GetComponent<starting_Input_Script>().getName());
        }
        table.GetComponentInChildren<GridLayoutGroup>().constraintCount = names.Count + numOutputBits;

        //setting the first cell to first name
        if (names[0].Length>0) {
            cell.GetComponentInChildren<TextMeshProUGUI>().SetText(names[0]);
        } else {
            cell.GetComponentInChildren<TextMeshProUGUI>().SetText("Input 1");
        }
        for (int i = 1; i<names.Count; i++) {
            GameObject columnCell = Instantiate(cell) as GameObject;
            columnCell.SetActive(true);
            if (names[i].Length>0) {
                columnCell.GetComponentInChildren<TextMeshProUGUI>().SetText(names[i]);
            } else {
                columnCell.GetComponentInChildren<TextMeshProUGUI>().SetText("Input " + (i+1).ToString());
            }
            columnCell.transform.SetParent(cell.transform.parent);
            columnCell.transform.localScale = cell.transform.localScale;
            columnCell.tag = "truthtablecomponent";
        }
        List<GameObject> startingOutputs = GameObject.Find("GameHandler").GetComponent<starting_outputs_handler>().outputs;
        List<string> outputNames = new List<string>();
        foreach(GameObject cout in startingOutputs) {
            outputNames.Add(cout.GetComponent<starting_Output_Script>().getName());
        }

        for (int i =0; i< numOutputBits; i++) {
            GameObject columnCell = Instantiate(cell) as GameObject;
            columnCell.SetActive(true);
            Debug.Log("Name: " + outputNames[i]);
            if (outputNames[i].Length>0) {
                columnCell.GetComponentInChildren<TextMeshProUGUI>().SetText(outputNames[i]);
            } else {
                columnCell.GetComponentInChildren<TextMeshProUGUI>().SetText("Output " + (i+1).ToString());
            }

            columnCell.transform.SetParent(cell.transform.parent);
            columnCell.transform.localScale = cell.transform.localScale;
            columnCell.tag = "truthtablecomponent";
        }

        for (int i = 0; i<inputPermutations.Count; i++) {
            for (int j = 0; j<inputPermutations[0].Length; j++) {
                GameObject columnCell = Instantiate(cell) as GameObject;
                columnCell.SetActive(true);
                columnCell.GetComponentInChildren<TextMeshProUGUI>().SetText(char.ToString(inputPermutations[i][j]));
                columnCell.transform.SetParent(cell.transform.parent);
                columnCell.transform.localScale = cell.transform.localScale;
                columnCell.tag = "truthtablecomponent";
            }
            for (int j = 0; j<outputPermutations[0].Length; j++) {
                GameObject columnCell = Instantiate(cell) as GameObject;
                columnCell.SetActive(true);
                columnCell.GetComponentInChildren<TextMeshProUGUI>().SetText(char.ToString(outputPermutations[i][j]));
                columnCell.transform.SetParent(cell.transform.parent);
                columnCell.transform.localScale = cell.transform.localScale;
                columnCell.tag = "truthtablecomponent";
            }
        }
        }
    }
    
}
