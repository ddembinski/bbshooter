using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour {

    public AudioClip noise1;

    public AudioSource audioS;


    public void PlayNoise1() {
        audioS.PlayOneShot(noise1);
    }
}
