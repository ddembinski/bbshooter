using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redRoboSound : MonoBehaviour {

    public AudioClip noise1;
    public AudioClip noise2;

    public AudioSource audio1;
    public AudioSource audio2;

    void PlayNoise1() {
        audio1.pitch = Random.Range(0.5f, 1.5f);
        audio1.PlayOneShot(noise1);
    }
    void PlayNoise2() {
        audio2.pitch = Random.Range(0.9f, 1.1f);
        audio2.PlayOneShot(noise2);
    }
}
