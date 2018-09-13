using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public AudioMixer audioMixer;
    public Slider volumeSlider;

	public void SetVolume(float volume) {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetSliderPosition() {
        float currentVolume;
        audioMixer.GetFloat("volume", out currentVolume);
        volumeSlider.GetComponent<Slider>().value = currentVolume;
    }

    void Awake() {
        SetSliderPosition();
    }
}
