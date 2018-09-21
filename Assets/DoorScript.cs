using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public bool isOpen = false;

	// Update is called once per frame
	void Update () {
        if (isOpen) {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetFloat("DoorState", 3);
        } else {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<Animator>().SetFloat("DoorState", 1);
        }

    }
}
