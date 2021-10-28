using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Logic : MonoBehaviour
{
    public GameObject startingObject;
    public GameObject endingObject;
    LineRenderer lr;
    float time;
    void Start() {
        lr = GetComponent<LineRenderer>();
        time = 0.0f;
    }


    public void updateLineEnds() {
        lr.SetPositions(new Vector3[] {startingObject.transform.position, endingObject.transform.position});
    }
}
