using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeChip : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chipPrefab;
    public GameObject buttonPrefab;
    public int numInputs;
    public int numOutputs;
    void Start()
    {
        numInputs=8;
        numOutputs = 1;
        GameObject chip = (GameObject)Instantiate(chipPrefab, Vector3.zero, Quaternion.identity);      
    }

    public void createChip(string name) {
        Load_States statesClass = gameObject.GetComponent<Load_States>();
        IDictionary<string, List<string>> states = statesClass.savedStates[name];
        List<string> inputs = states["InputStates"];
        numInputs = inputs[0].Length;
        List<string> outputs =states["OutputStates"];
        numOutputs = outputs[0].Length;
        GameObject newChip = Instantiate(chipPrefab, Camera.main.transform.position + new Vector3(0,0,21), Quaternion.identity);

        List<string> loadedInputNames = states["InputNames"];
        List<string> loadedOutputNames = states["OutputNames"];
        newChip.GetComponent<Chip>().create(name, numInputs, numOutputs, loadedInputNames, loadedOutputNames);
        newChip.GetComponent<Chip>().chipName = name;
    }
}
