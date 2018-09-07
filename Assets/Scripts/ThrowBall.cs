using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {

    Animator anim;
    
    public float throwRate = 0;
    public float Damage = 1;
    public LayerMask whatToHit;
    public GameObject projectile;
    public float velocity = 5;
    private float torque = 0f;
    int english = 0;

    //float timeToThrow = 0;
    Transform releasePoint;

	// Use this for initialization
	void Awake () {
        releasePoint = transform.Find("ReleasePoint");
        if (releasePoint == null) {
            Debug.LogError("No releasePoint found... !?");
        }

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
		if (throwRate == 0) {
            if (Input.GetButtonDown ("Fire1") && (Input.GetAxis("Horizontal") < 0)) {
                torque = -900f;
                english = -1;
                ThrowIt();
            } else if (Input.GetButtonDown ("Fire1") && (Input.GetAxis("Horizontal") > 0)) {
                torque = 900f;
                english = 1;
                ThrowIt();
            } else if (Input.GetButtonDown("Fire1")) {
                torque = 0f;
                english = 0;
                ThrowIt();
            }
        } 
        if (Input.GetButtonUp("Fire1") && anim.GetBool("Throw")) {
            anim.SetBool("Throw", false);
        }
    }

    void ThrowIt() {
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
        ball.GetComponent<Rigidbody2D>().AddForce(normalizedDirection * velocity);
        ball.GetComponent<Rigidbody2D>().AddTorque(torque, ForceMode2D.Impulse);
        ball.GetComponent<BallScript>().english = english;
        Destroy(ball, 5f);
    }

    void LastUpdate() {

    }

}
