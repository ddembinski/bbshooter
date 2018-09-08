using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenRoboScript : MonoBehaviour {

    Animator anim;

    public float timeOut = 3;
    public bool needsReset = true;
    public bool active;
    private GameObject player;

    public void OnCollisionEnter2D(Collision2D collision) {
        //if (collision.gameObject.tag == "LiveBall") {
            collision.gameObject.tag = "DeadBall";
            anim.SetBool("isHit", true);
            //collision.gameObject.layer = 8;
            gameObject.layer = 8;
            active = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -0.5f);
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(collision.gameObject, 0.25f);
            Destroy(gameObject, 1.35f);
        //}
    }
    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
        //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.5f);
        active = true;
        needsReset = true;
        player = GameObject.Find("Truck");
    }

    // Update is called once per frame
    void Update () {
        TruckScript ts = player.GetComponent<TruckScript>();
        bool isReset = ts.reset;
        if (isReset && needsReset && active) {
            transform.Translate(0, -15, 0);
            needsReset = false;
        }
	}

}
