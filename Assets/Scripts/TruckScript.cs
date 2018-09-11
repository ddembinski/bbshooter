using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckScript : MonoBehaviour {

    public float moveSpeed = 0.0000001f;
    public float forwardSpeed = 0.0000001f;
    public bool reset = false;
    public bool endless = true;
    public int activeSceneIndex;
    public bool autoForward = true;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 movement = Vector2.zero;

        if (autoForward) {
            movement.y = (transform.up * Time.deltaTime * forwardSpeed).y;
        }

        if (Input.GetAxisRaw("Horizontal") < 0) {
            //transform.Translate((truckSpeed * Time.deltaTime) * -1, 0, 0);
            movement.x = (transform.right * Time.deltaTime * -moveSpeed).x;
        } else if (Input.GetAxisRaw("Horizontal") > 0) {
            //transform.Translate(truckSpeed * Time.deltaTime, 0, 0);
            movement.x = (transform.right * Time.deltaTime * moveSpeed).x;
        }
        if (Input.GetAxisRaw("Vertical") < 0) {
            //transform.Translate(0, (truckSpeed * Time.deltaTime) * -1, 0);
            movement.y = (transform.up * Time.deltaTime * -moveSpeed).y;
        } else if (Input.GetAxisRaw("Vertical") > 0) {
            //transform.Translate(0, (truckSpeed * Time.deltaTime) * 1, 0);
            movement.y = (transform.up * Time.deltaTime * moveSpeed).y;
        }

        movement = movement + (Vector2)(transform.position);

        gameObject.GetComponent<Rigidbody2D>().MovePosition(movement);
        //rigidbody2D.MovePosition(movement);

        if (Input.GetKey(KeyCode.Escape)) {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(activeSceneIndex);
            SceneManager.LoadScene("MainMenu");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        }



        //transform.Translate(0, forwardSpeed * Time.deltaTime, 0);

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
