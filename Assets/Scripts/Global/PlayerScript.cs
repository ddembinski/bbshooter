using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    Animator anim;

    public float moveSpeed = 0.0000001f;
    public bool canMove = true;
    public bool reset = false;
    public int activeSceneIndex;
    public int hitPoints = 1;
    public bool wasHit = false;
    public bool gamePaused = false;


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

        /*Debug.Log("Something's collided with us. It's " + collision.gameObject.name);
        Debug.Log("collision's layer: " + collision.gameObject.layer);
        Debug.Log("Our layer: " + gameObject.layer);*/

    }

    // Use this for initialization
    void Start() {
        reset = false;
        wasHit = false;
        //gameObject.layer = 0;
        gameObject.GetComponent<Collider2D>().enabled = true;
        anim = GetComponent<Animator>();
        gamePaused = false;
    }

    // Update is called once per frame
    void Update() {

        if (gamePaused) {
            Time.timeScale = 0;
        }

        if (gamePaused && Input.GetKeyDown("p")) {
            gamePaused = false;
            Time.timeScale = 1;
        } else if (!gamePaused && Input.GetKeyDown("p")) {
            gamePaused = true;
        }

        if (hitPoints <= 0) {
            StartCoroutine(WaitToDie(2));
            Camera.main.GetComponent<CameraEffects>().Zoom();
        }
        if (canMove) {
            Vector2 movement = Vector2.zero;

            if (Input.GetAxisRaw("Horizontal") < 0) {
                anim.SetFloat("MoveVertical", 2.0f);
                anim.SetFloat("MoveHorizontal", 0.5f);
                //transform.Translate((truckSpeed * Time.deltaTime) * -1, 0, 0);
                movement.x = (transform.right * Time.deltaTime * -moveSpeed).x;
            } else if (Input.GetAxisRaw("Horizontal") > 0) {
                anim.SetFloat("MoveVertical", 2.0f);
                anim.SetFloat("MoveHorizontal", 3.5f);
                //transform.Translate(truckSpeed * Time.deltaTime, 0, 0);
                movement.x = (transform.right * Time.deltaTime * moveSpeed).x;
            } else if (Input.GetAxisRaw("Horizontal") == 0) {
                anim.SetFloat("MoveHorizontal", 2.0f);
            }
                if (Input.GetAxisRaw("Vertical") < 0) {
                anim.SetFloat("MoveHorizontal", 2.0f);
                anim.SetFloat("MoveVertical", 0.5f);
                //transform.Translate(0, (truckSpeed * Time.deltaTime) * -1, 0);
                movement.y = (transform.up * Time.deltaTime * -moveSpeed).y;
            } else if (Input.GetAxisRaw("Vertical") > 0) {
                anim.SetFloat("MoveHorizontal", 2.0f);
                anim.SetFloat("MoveVertical", 3.5f);
                //transform.Translate(0, (truckSpeed * Time.deltaTime) * 1, 0);
                movement.y = (transform.up * Time.deltaTime * moveSpeed).y;
            } else if (Input.GetAxisRaw("Vertical") == 0) {
                anim.SetFloat("MoveVertical", 2.0f);
            }
            movement = movement + (Vector2)(transform.position);
            gameObject.GetComponent<Rigidbody2D>().MovePosition(movement);
        }

        //rigidbody2D.MovePosition(movement);

        /*if (Input.GetKey(KeyCode.Escape)) {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(activeSceneIndex);
            SceneManager.LoadScene("MainMenu");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        }*/

        if (wasHit) {
            StartCoroutine(DamageImmunity(3));
            wasHit = false;
        }

    }

    private IEnumerator WaitToDie(float seconds) {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
