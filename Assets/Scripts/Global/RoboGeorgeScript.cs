using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboGeorgeScript : MonoBehaviour {

    Animator anim;

    public float timeOut = 3;
    public bool active;
    public int hitPoints = 2;
    public bool wasHit = false;
    public bool seesTarget = false;
    public bool canMove = true;
    public float walkSpeed;
    public float regenLength = 10;
    private Vector2 originalPosition;
    public bool atOrigin = true;
    private GameObject player;
    //private CameraEffects camEffects;

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "LiveBall") {
            //collision.gameObject.tag = "DeadBall";
            //collision.gameObject.layer = 8;
            hitPoints--;
            gameObject.layer = 8;
            active = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (collision.gameObject.GetComponent<Rigidbody2D>().mass > 1) {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                //camEffects.Shake(0.1f);
                Destroy(collision.gameObject, 0.2f);
                anim.SetBool("Destroy", true);
                Destroy(gameObject, 0.5f);

            }

            if (hitPoints >= 1) {
                anim.SetBool("IsHit", true);
                wasHit = true;
                canMove = false;
                Destroy(collision.gameObject, 0.25f);
                StartCoroutine(RegenHealth(regenLength));
            } else {
                anim.SetBool("Destroy", true);
                Destroy(collision.gameObject, 0.25f);
                Destroy(gameObject, 0.9f);
            }


        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "StopPoint") {
            Debug.Log("Collided with StopPoint. Collider name: " + gameObject.name.ToString());
            if (gameObject.tag != "Vision") {
                atOrigin = true;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            seesTarget = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            seesTarget = false;
        }
        if (collision.gameObject.tag == "StopPoint") {
            if (gameObject.tag != "Vision") {
                atOrigin = false;
            }
        }
    }

    private IEnumerator DamageImmunity(float seconds) {
        Debug.Log("Immune to damage for " + seconds + " seconds");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Wait over, removing damage immunity");
        gameObject.layer = 0;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private IEnumerator RegenHealth(float seconds) {
        yield return new WaitForSeconds(seconds);
        hitPoints++;
        anim.SetBool("IsHit", false);
        canMove = true;
    }

    // Use this for initialization
    void Awake() {
        anim = GetComponent<Animator>();
        //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.5f);
        active = true;
        player = GameObject.Find("Player");
        //camEffects = Camera.main.GetComponent<CameraEffects>();
        seesTarget = false;
        originalPosition = new Vector2(transform.position.x, transform.position.y);
        walkSpeed = 1f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (wasHit) {
            Debug.Log("wasHit true, invoking damage immunity");
            StartCoroutine(DamageImmunity(1));
            wasHit = false;
        }

        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        if (seesTarget && canMove) {
            //Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 normalizedDirection = (targetPosition - currentPosition).normalized;
            gameObject.GetComponent<Rigidbody2D>().velocity = (normalizedDirection * walkSpeed);
            anim.SetFloat("RunVertical", 0.5f);
            //Debug.Log("Target sighted. Engaging.");

        } else if (atOrigin) {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //Debug.Log("At origin. Going into standby.");

        } else if (canMove) {
            //Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPosition = originalPosition;
            Vector2 normalizedDirection = (targetPosition - currentPosition).normalized;
            gameObject.GetComponent<Rigidbody2D>().velocity = normalizedDirection;
            anim.SetFloat("RunVertical", 3.5f);
            //Debug.Log("Target lost. Returning to origin");
            //Debug.Log("Current point: " + currentPosition.ToString());
            //Debug.Log("Origin point: " + originalPosition.ToString());

        } else {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (GetComponent<Rigidbody2D>().velocity == Vector2.zero) {
            anim.SetFloat("RunVertical", 2.0f);
        }

    }

}
