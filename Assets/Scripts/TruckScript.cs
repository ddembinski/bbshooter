using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckScript : MonoBehaviour {

    public float truckSpeed = 0.0000001f;
    public float forwardSpeed = 0.0000001f;
    public bool reset = false;
    public bool endless = true;
    public int activeSceneIndex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Horizontal") < 0) {
            transform.Translate((truckSpeed * Time.deltaTime) * -1, 0,  0);
        } else if (Input.GetAxisRaw("Horizontal") > 0) {
            transform.Translate(truckSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.Escape)) {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(activeSceneIndex);
            SceneManager.LoadScene("MainMenu");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        }



        transform.Translate(0, forwardSpeed * Time.deltaTime, 0);

        if ((transform.position.y > 10) && endless) {
            reset = true;
            transform.Translate(0, -15, 0);
            StartCoroutine(PostReset());
        }
    }

    IEnumerator PostReset() {
        yield return new WaitForSeconds(0.05f);
        reset = false;
    }
}
