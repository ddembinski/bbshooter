using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L3ExitTrigger : MonoBehaviour {

    int nextSceneIndex; 

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Collider2D>().name == "Player") {
            Debug.Log("Loading Next Scene");
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    void Awake() {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }
}
