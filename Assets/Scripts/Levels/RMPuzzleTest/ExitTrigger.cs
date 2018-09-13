using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Collider2D>().name == "Player") {
            Debug.Log("Loading Next Scene");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
