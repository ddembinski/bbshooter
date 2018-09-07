using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour {

    public float truckSpeed = 0.0000001f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Horizontal") < 0) {
            transform.Translate((truckSpeed * Time.deltaTime) * -1, 0,  0);
        } else if (Input.GetAxisRaw("Horizontal") > 0) {
            transform.Translate(truckSpeed * Time.deltaTime, 0, 0);
        }
    }
}
