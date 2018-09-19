using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSound : MonoBehaviour {

    public AudioClip noise1;

    public AudioSource audioS;


    public void PlayNoise() {
        audioS.PlayOneShot(noise1);
    }
}
