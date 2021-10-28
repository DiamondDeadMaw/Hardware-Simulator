using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using System.IO;
 using System.Runtime.Serialization.Formatters.Binary;

//goes through all binary permutations of inputs and hardcodes them to the chip
public class save_States : MonoBehaviour
{
    int[] arr;
    List<string> inputPermutations;
    List<string> outputPermutations;
    string chipName;
    public GameObject inputFieldPrefab;
    GameObject canvas;

    public void saveStates() {
        List<GameObject> inputs = gameObject.GetComponent<starting_inputs_handler>().inputs;
        List<GameObject> outputs = gameObject.GetComponent<starting_outputs_handler>().outputs;
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

        //instantiate inputfield and accept input
        canvas = Instantiate(inputFieldPrefab, Camera.main.transform.position, Quaternion.identity);
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        InputField ifield = canvas.GetComponentInChildren<InputField>();
        ifield.onEndEdit.AddListener(delegate{acceptInput(ifield);});     

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
    public void acceptInput(InputField itext) {
        chipName = itext.text.ToUpper();
        Destroy(canvas);


        //serializing object here as for some reason this method runs on a new thread
        string file_path = Application.persistentDataPath + "/" + chipName + ".dat";
        FileStream file;
        file = File.Create(file_path);

        //getting names of starting input sockets
        List<GameObject> startingInputs = GameObject.Find("GameHandler").GetComponent<starting_inputs_handler>().inputs;
        List<string> startingInputNames = new List<string>();
        foreach (GameObject inputButton in startingInputs) {
            startingInputNames.Add(inputButton.GetComponent<starting_Input_Script>().getName());
        }

        //getting names of starting output sockets
        List<GameObject> startingOutputs = GameObject.Find("GameHandler").GetComponent<starting_outputs_handler>().outputs;
        List<string> startingOutputNames = new List<string>();
        foreach (GameObject outputButton in startingOutputs) {
            startingOutputNames.Add(outputButton.GetComponent<starting_Output_Script>().getName());
        }

        toSerialize save_data = new toSerialize(inputPermutations, outputPermutations, startingInputNames, startingOutputNames);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, save_data);
        file.Close();
        gameObject.GetComponent<Load_States>().Start();
    }
}

[System.Serializable]
public class toSerialize {
    public List<string> inputStates;
    public List<string> outputStates;
    public List<string> inputNames;
    public List<string> outputNames;

    public toSerialize(List<string> inputSt, List<string> outputSt, List<string> iNames, List<string> oNames) {
        inputStates = inputSt;
        outputStates = outputSt;
        inputNames = iNames;
        outputNames = oNames;
    }
}
