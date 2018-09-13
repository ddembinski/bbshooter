using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public GameObject switchOn;


    public GameObject switchOff;

    public bool isOn = false;

	void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Collider2D>().name != "ParryCollider" && collision.GetComponent<Collider2D>().name != "Vision") {
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;
            isOn = true;
            gameObject.GetComponent<SwitchSound>().PlayNoise();

        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<Collider2D>().name != "ParryCollider") {
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
            isOn = false;

        }
    }
}
