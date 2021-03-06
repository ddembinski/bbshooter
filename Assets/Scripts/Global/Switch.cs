﻿using System.Collections;
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
        if (collision.GetComponent<Collider2D>().name != "Vision") {
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;
            isOn = true;
            gameObject.GetComponent<SwitchSound>().PlayNoise();

        }
    }

    void OnTriggerStay2D(Collider2D collision) {
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;
            isOn = true;
    }

    void OnTriggerExit2D(Collider2D collision) {
        StartCoroutine(SwitchDelayOff(0.7f));
    }

    public void SwitchOff() {
        StartCoroutine(SwitchDelayOff(0.7f));
    }

    private IEnumerator SwitchDelayOff(float seconds) {
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
        isOn = false;
    }
}
