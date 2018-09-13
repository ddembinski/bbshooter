using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRoboScript : MonoBehaviour {
    Animator anim;

    public float timeOut = 3;
    public bool active;
    public int hitPoints = 2;
    public bool wasHit = false;
    public bool needsReset = true;
    private int throwTimer = 90;
    private int throwCooldown = 240;
    private bool canThrow = true;
    private bool seesTarget = false;
    public float velocity = 2;
    public GameObject player;
    //public CameraEffects camEffects;
    //private CameraEffects camEffects;
    public GameObject projectile;
    //Transform releasePoint;

    public void OnCollisionEnter2D(Collision2D collision) {
        //if (collision.gameObject.tag == "EnemyProjectile") {
        //    Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
        //}
        if (collision.gameObject.tag == "LiveBall") {
            collision.gameObject.tag = "DeadBall";
            collision.gameObject.layer = 8;
            hitPoints--;
            gameObject.layer = 8;
            active = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (collision.gameObject.GetComponent<Rigidbody2D>().mass > 1) {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                Destroy(collision.gameObject);
                anim.SetBool("Destroy", true);
                Destroy(gameObject, 0.5f);

            }
            if (hitPoints >= 1) {
                anim.SetBool("isHit", true);
                wasHit = true;
                canThrow = false;
                StartCoroutine(RegenHealth(5));
            } else {
                anim.SetBool("Destroy", true);
                Destroy(collision.gameObject, 0.25f);
                Destroy(gameObject, 0.5f);
            }



        }

    }

    public void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            seesTarget = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        seesTarget = false;
    }

    private IEnumerator DamageImmunity(float seconds) {
        Debug.Log("Immune to damage for " + seconds + " seconds");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Wait over, removing damage immunity");
        gameObject.layer = 12;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private IEnumerator RegenHealth(float seconds) {
        yield return new WaitForSeconds(seconds);
        canThrow = true;
        hitPoints++;
        anim.SetBool("isHit", false);
    }

    void ThrowBall() {
        Vector2 throwPointPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 normalizedDirection = (targetPosition - throwPointPosition).normalized;
        GameObject ball = (GameObject)Instantiate(projectile, throwPointPosition, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().mass = 2;
        ball.GetComponent<Rigidbody2D>().velocity = (normalizedDirection * velocity);
        Destroy(ball, 5f);
        throwTimer = throwCooldown;
    }

    // Use this for initialization
    void Awake() {
        anim = GetComponent<Animator>();
        //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -1);
        active = true;
        needsReset = true;
        player = GameObject.Find("Player");
        //camEffects = Camera.main.GetComponent<CameraEffects>();
        //releasePoint = transform.Find("RedRoboReleasePoint");
        seesTarget = false;
    }

    // Update is called once per frame
    void Update() {
        if (wasHit) {
            Debug.Log("wasHit true, invoking damage immunity");
            StartCoroutine(DamageImmunity(1));
            wasHit = false;
        }

        if ((wasHit != true) && canThrow && seesTarget) {
            if (throwTimer <= 0) {
                ThrowBall();
            } else {
                throwTimer--;
            }
        }

    }
}
