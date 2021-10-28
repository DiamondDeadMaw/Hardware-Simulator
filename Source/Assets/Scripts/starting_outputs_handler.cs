using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starting_outputs_handler : MonoBehaviour
{
    public List<GameObject> outputs;
    public GameObject outputPrefab;
    Camera cam;
    GameObject output;
    public void Start()
    {
        cam = Camera.main;
        outputs = new List<GameObject>();
        float size = cam.orthographicSize;

        float xPos = cam.transform.position.x;
        float yPos = cam.transform.position.y;
        float radius = outputPrefab.GetComponent<SpriteRenderer>().bounds.size.x/2;

        output = (GameObject)Instantiate(outputPrefab, new Vector3(xPos+size*cam.aspect,yPos,0), Quaternion.identity);
        outputs.Add(output); 
        gameObject.GetComponent<CameraController>().updateElements();
    }

    public void addNewOutputButton(bool remove) {
        output = Instantiate(outputPrefab, new Vector3(0,0,cam.transform.position.z-1), Quaternion.identity);
        if (!remove) {
            outputs.Add(output);
        } else {
            Destroy(output);
            GameObject toRemove = outputs[outputs.Count-1];
            outputs.RemoveAt(outputs.Count-1);
            Destroy(toRemove);
        }
        
        output.transform.localScale = outputs[0].transform.localScale;

        int numoutputs = outputs.Count;
        float maxHeight = cam.orthographicSize;
        
        float spaceBw = 0.2f;
        float xPos = cam.transform.position.x;
        float yPos = cam.transform.position.y;
        float radius = outputs[0].GetComponent<SpriteRenderer>().bounds.size.x/2;
        float heightOfoutputs = spaceBw + (numoutputs-1)*spaceBw + numoutputs*radius;
        if (heightOfoutputs>maxHeight) {
            float newRadius = (maxHeight - spaceBw - (numoutputs-1)*spaceBw)/numoutputs;
            float ratio = newRadius/radius;
            for (int i = 0; i< numoutputs; i++) {
                Vector3 scaleInit = outputs[i].transform.localScale;
                Vector3 newScale = scaleInit*ratio;
                scaleInit.Set(newScale.x, newScale.y, newScale.z);
                outputs[i].transform.localScale = newScale;
            }
        }
        radius = outputs[0].GetComponent<SpriteRenderer>().bounds.size.x/2;
        if (numoutputs%2==0) {
            for (int i =numoutputs/2-1; i>=0; i--) {
                outputs[numoutputs/2 - i- 1].transform.position = new Vector3(xPos+maxHeight*cam.aspect - radius/2,yPos+ spaceBw/2 + radius + (2*radius + spaceBw)*i, 0);
            }
            
            for (int i = 0; i< numoutputs/2; i++) {
                outputs[numoutputs/2 + i].transform.position = new Vector3(xPos+maxHeight*cam.aspect - radius/2,yPos- spaceBw/2 - radius - (2*radius + spaceBw)*i, 0);
            }
        } else if (numoutputs%2 != 0) {
            for (int i = numoutputs/2 -1; i>=0; i--) {
                outputs[numoutputs/2 - i - 1].transform.position = new Vector3(xPos+maxHeight*cam.aspect - radius/2,yPos+ 2*radius + spaceBw + (2*radius + spaceBw)*i, 0);
            }
            outputs[numoutputs/2].transform.position = new Vector3(xPos+maxHeight*cam.aspect - radius/2, yPos, 0);
            for (int i =0; i<numoutputs/2;i++) {
                outputs[numoutputs/2 + i + 1].transform.position = new Vector3(xPos+maxHeight*cam.aspect - radius/2, yPos - 2*radius - spaceBw - (2*radius+spaceBw)*i, 0);
            }
        } 
        gameObject.GetComponent<CameraController>().updateElements();
    }

}
