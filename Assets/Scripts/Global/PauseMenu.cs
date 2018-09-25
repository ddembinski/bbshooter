using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject fader;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
	}

    public void Resume() {
        pauseMenuUI.SetActive(false);
        fader.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        fader.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResetRoom() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
