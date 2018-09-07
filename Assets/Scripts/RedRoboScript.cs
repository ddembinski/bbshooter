using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRoboScript : MonoBehaviour {
    Animator anim;

    public float timeOut = 3;

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "LiveBall") {
            collision.gameObject.tag = "DeadBall";
            collision.gameObject.layer = 8;
            gameObject.layer = 8;
            anim.SetBool("isHit", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -1.5f);
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(collision.gameObject, 0.25f);
            Destroy(gameObject, 1.5f);
        }

    }
    // Use this for initialization
    void Awake() {
        anim = GetComponent<Animator>();
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -1);
    }

    // Update is called once per frame
    void Update() {

    }
}
