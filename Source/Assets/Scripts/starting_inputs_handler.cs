using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starting_inputs_handler : MonoBehaviour
{
    public List<GameObject> inputs = new List<GameObject>();
    public GameObject starting_input_prefab;
    GameObject addButton;
    GameObject input;
    Camera cam;
    public void Start()
    {   
        cam = Camera.main;
        float size = cam.orthographicSize;
        addButton = GameObject.FindGameObjectWithTag("add_starting_inputs");


        float spaceBW = 0.5f;
        float xPos = cam.transform.position.x;
        float yPos = cam.transform.position.y;
        float radius = starting_input_prefab.GetComponent<SpriteRenderer>().bounds.size.x/2;

        input = (GameObject)Instantiate(starting_input_prefab, new Vector3(xPos- size*cam.aspect, yPos + spaceBW/2 + radius,0), Quaternion.identity);
        inputs.Add(input); 
        input = (GameObject)Instantiate(starting_input_prefab, new Vector3(xPos- size*cam.aspect, yPos - spaceBW/2 - radius,0), Quaternion.identity);
        inputs.Add(input);
        gameObject.GetComponent<CameraController>().updateElements(); 
        
    }

    public void addNewInputButton(bool remove) {
        input = Instantiate(starting_input_prefab, new Vector3(0,0,cam.transform.position.z-1), Quaternion.identity);
        if (!remove) {
            inputs.Add(input);
        } else {
            Destroy(input);
            GameObject toRemove = inputs[inputs.Count-1];
            inputs.RemoveAt(inputs.Count-1);
            Destroy(toRemove);
        }
        
        input.transform.localScale = inputs[0].transform.localScale;

        int numInputs = inputs.Count;
        float maxHeight = cam.orthographicSize;
        
        float spaceBw = 0.2f;
        float xPos = cam.transform.position.x;
        float yPos = cam.transform.position.y;
        float radius = inputs[0].GetComponent<SpriteRenderer>().bounds.size.x/2;
        float heightOfInputs = spaceBw + (numInputs-1)*spaceBw + numInputs*radius;
        if (heightOfInputs>maxHeight) {
            float newRadius = (maxHeight - spaceBw - (numInputs-1)*spaceBw)/numInputs;
            float ratio = newRadius/radius;
            for (int i = 0; i< numInputs; i++) {
                Vector3 scaleInit = inputs[i].transform.localScale;
                Vector3 newScale = scaleInit*ratio;
                scaleInit.Set(newScale.x, newScale.y, newScale.z);
                inputs[i].transform.localScale = newScale;
            }
        }
        //creates them top to down
        radius = inputs[0].GetComponent<SpriteRenderer>().bounds.size.x/2;
        if (numInputs%2==0) {
            for (int i =numInputs/2-1; i>=0; i--) {
                inputs[numInputs/2 - i- 1].transform.position = new Vector3(xPos-maxHeight*cam.aspect + radius/2,yPos+ spaceBw/2 + radius + (2*radius + spaceBw)*i, 0);
            }
            
            for (int i = 0; i< numInputs/2; i++) {
                inputs[numInputs/2 + i].transform.position = new Vector3(xPos-maxHeight*cam.aspect + radius/2,yPos- spaceBw/2 - radius - (2*radius + spaceBw)*i, 0);
            }
        } else if (numInputs%2 != 0) {
            for (int i = numInputs/2 -1; i>=0; i--) {
                inputs[numInputs/2 - i - 1].transform.position = new Vector3(xPos-maxHeight*cam.aspect + radius/2,yPos+ 2*radius + spaceBw + (2*radius + spaceBw)*i, 0);
            }
            inputs[numInputs/2].transform.position = new Vector3(xPos-maxHeight*cam.aspect + radius/2, yPos, 0);
            for (int i =0; i<numInputs/2;i++) {
                inputs[numInputs/2 + i + 1].transform.position = new Vector3(xPos- maxHeight*cam.aspect + radius/2, yPos - 2*radius - spaceBw - (2*radius+spaceBw)*i, 0);
            }
        } 
        gameObject.GetComponent<CameraController>().updateElements();
    }
}