using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensorScript : MonoBehaviour {

    public string willAllowThrough;

    public void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == willAllowThrough) {
            GetComponentInParent<DoorScript>().isOpen = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == willAllowThrough ) {
            GetComponentInParent<DoorScript>().isOpen = false;
        }
    }
}
