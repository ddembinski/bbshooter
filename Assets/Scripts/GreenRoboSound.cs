using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenRoboSound : MonoBehaviour {
    public AudioClip noise1;
    public AudioClip noise2;

    public AudioSource audioS;
    

    void PlayNoise1() {
        audioS.pitch = Random.Range(0.5f, 1.5f);
        audioS.PlayOneShot(noise1);
    }
    void PlayNoise2() {
        audioS.pitch = Random.Range(0.9f, 1.1f);
        audioS.PlayOneShot(noise2);
    }
}
