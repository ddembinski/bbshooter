using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {

    Animator anim;
    
    public float throwRate = 0;
    public float Damage = 1;
    public LayerMask whatToHit;
    public GameObject projectile;
    private CameraEffects camEffects;
    public float velocity = 5;
    public float slowdownFactor = 0.0f;
    public float slowdownLength = 2f;
    public float ballLifetime = 5f;
    //private float torque = 0f;
    float mass = 1f;
    int english = 0;
    float chargeTimer = 0f;
    float maxCharge = 1.1f;
    private bool justThrown = false;
    public bool canParry = false;

    //float timeToThrow = 0;
    Transform releasePoint;

    public void OnTriggerStay2D(Collider2D collision) {
        if ((collision.gameObject.tag == "EnemyProjectile")) {
            if (Input.GetButtonDown("Fire2")) {
                Debug.Log("parrying");
                Time.timeScale = slowdownFactor;
                camEffects.Shake(0.2f);
                //camEffects.Zoom();
                collision.GetComponent<Rigidbody2D>().mass *= 10;
                collision.GetComponent<Rigidbody2D>().velocity = (collision.GetComponent<Rigidbody2D>().velocity * -1) * 10;
                collision.gameObject.tag = "LiveBall";
                collision.gameObject.layer = 9;
                collision.GetComponent<TrailRenderer>().enabled = true;
                //collision.transform.localScale = new Vector3(3, 3, 0);

            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyProjectile") {
            //canParry = false;
        }
    }


    // Use this for initialization
    void Awake () {
        releasePoint = transform.Find("ReleasePoint");
        if (releasePoint == null) {
            Debug.LogError("No releasePoint found... !?");
        }

        anim = GetComponent<Animator>();
        projectile.GetComponent<TrailRenderer>().enabled = false;
        camEffects = Camera.main.GetComponent<CameraEffects>();
    }
	
	// Update is called once per frame
	void Update () {

        /*if (Time.timeScale < 1) {
            Time.timeScale = ((float)Time.timeScale + (1.0f / slowdownLength) * Time.unscaledDeltaTime);
        }*/

        if (throwRate == 0) {
            if (Input.GetButtonDown ("Fire1") && (Input.GetAxis("Horizontal") < 0)) {
                //torque = -100f;
                english = -1;
                ThrowIt();
            } else if (Input.GetButtonDown ("Fire1") && (Input.GetAxis("Horizontal") > 0)) {
                //torque = 100f;
                english = 1;
                ThrowIt();
            } else if (Input.GetButtonDown("Fire1")) {
                //torque = 0f;
                english = 0;
                ThrowIt();
            }
        } 
        if (Input.GetButtonUp("Fire1") && anim.GetBool("Throw")) {
            anim.SetBool("Throw", false);
        }
        if (anim.GetBool("Throw") && justThrown) {
            anim.SetBool("Throw", false);
            justThrown = false;
        }

        //if (Input.GetButtonDown("Fire2")) {
        //    canParry = true;
        //}

        if (Input.GetButton("Fire2") && chargeTimer < maxCharge) {
            anim.SetBool("WindUp", true);
            chargeTimer += Time.deltaTime;
            projectile.GetComponent<TrailRenderer>().enabled = true;
        } else if (Input.GetButton("Fire2") && chargeTimer >= maxCharge) {
            chargeTimer = 1.5f;
        } 

        if (Input.GetButtonUp("Fire2") && chargeTimer > (maxCharge - 0.1f)) {
            
            float prevVelocity = velocity;
            float prevMass = mass;
            mass *= 10;
            velocity *= 2;
            ThrowIt();
            velocity = prevVelocity;
            mass = prevMass;
            chargeTimer = 0.0f;
            justThrown = true;
            projectile.GetComponent<TrailRenderer>().enabled = false;

        } else if (Input.GetButtonUp("Fire2") && chargeTimer < (maxCharge - 0.1f)) {
            chargeTimer = 0.0f;
            anim.SetBool("WindUp", false);
            projectile.GetComponent<TrailRenderer>().enabled = false;
        }

    }

    void ThrowIt() {
        anim.SetBool("WindUp", false);
        anim.SetBool("Throw", true);
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 throwPointPosition = new Vector2(releasePoint.position.x, releasePoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(throwPointPosition, mousePosition - throwPointPosition, 1000, whatToHit);
        //Debug.DrawLine(throwPointPosition, (mousePosition-throwPointPosition)*100, Color.cyan);
        Vector2 normalizedDirection = (mousePosition - throwPointPosition).normalized;
        //if (hit.collider != null) {
            // Debug.DrawLine(throwPointPosition, hit.point, Color.black);
            //Debug.Log("Ball hit " + hit.collider.name + " and did " + Damage + " damage.");
        //}
        GameObject ball = (GameObject)Instantiate(projectile, throwPointPosition, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().mass = mass;
        //ball.GetComponent<Rigidbody2D>().AddForce(normalizedDirection * velocity);
        ball.GetComponent<Rigidbody2D>().velocity = (normalizedDirection * velocity);
        //ball.GetComponent<Rigidbody2D>().AddTorque(torque, ForceMode2D.Impulse);
        ball.GetComponent<BallScript>().english = english;
        Destroy(ball, ballLifetime);
        
    }

    void LastUpdate() {

    }


}
