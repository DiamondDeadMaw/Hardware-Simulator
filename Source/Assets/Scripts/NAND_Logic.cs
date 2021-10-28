using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAND_Logic : MonoBehaviour
{
    public InputSocket[] inputs;
    public Output output;

    string chip_name = "NAND";

    public void eval() {
        output.updateState(!(inputs[0].state && inputs[1].state));       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
