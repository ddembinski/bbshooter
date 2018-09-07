using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public int english = 0;
    Vector3 curveLeft = Vector3.left;
    Vector3 curveRight = Vector3.right;

    // Use this for initialization
    void Start () {
        if (english <= -1) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(curveLeft * 100);
        } else if (english >= 1) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(curveRight * 100);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
    }
}
