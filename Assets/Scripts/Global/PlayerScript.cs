using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public float moveSpeed = 0.0000001f;
    public bool canMove = true;
    public bool reset = false;
    public int activeSceneIndex;
    public int hitPoints = 2;
    public bool wasHit = false;


    public void OnCollisionEnter2D(Collision2D collision) {
        if ((collision.gameObject.tag == "Enemy") || (collision.gameObject.tag == "EnemyProjectile")) {
            //collision.gameObject.tag = "DeadBall";
            //collision.gameObject.layer = 8;
            hitPoints--;
            gameObject.layer = 8;
            gameObject.GetComponent<Collider2D>().enabled = false;
            wasHit = true;
            canMove = false;
            gameObject.GetComponent<ThrowBall>().canParry = false;
            StartCoroutine(RegenHealth(10));
        }

    }

    // Use this for initialization
    void Start() {
        reset = false;
        hitPoints = 2;
        wasHit = false;
        gameObject.layer = 0;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update() {

        if (hitPoints <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (canMove) {
            Vector2 movement = Vector2.zero;

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
        }

        //rigidbody2D.MovePosition(movement);

        if (Input.GetKey(KeyCode.Escape)) {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(activeSceneIndex);
            SceneManager.LoadScene("MainMenu");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        }

        if (wasHit) {
            StartCoroutine(DamageImmunity(3));
            wasHit = false;
        }



    }

    private IEnumerator DamageImmunity(float seconds) {
        yield return new WaitForSeconds(seconds);
        gameObject.layer = 0;
        gameObject.GetComponent<Collider2D>().enabled = true;
        canMove = true;
    }

    private IEnumerator RegenHealth(float seconds) {
        Debug.Log("Regenerating hitPoints in " + seconds + " seconds");
        yield return new WaitForSeconds(seconds);
        Debug.Log("hitPoints regenerated");
        hitPoints++;
    }


    void LastUpdate() {

    }
}
