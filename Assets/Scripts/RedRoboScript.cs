using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRoboScript : MonoBehaviour {
    Animator anim;

    public float timeOut = 3;
    public bool active;
    public bool needsReset = true;
    private GameObject player;
    //public CameraEffects camEffects;
    private CameraEffects camEffects; 
    //public GameObject ball;
    
    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "LiveBall") {
            //collision.gameObject.tag = "DeadBall";
            //collision.gameObject.layer = 8;
            gameObject.layer = 8;
            active = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (collision.gameObject.GetComponent<Rigidbody2D>().mass > 1) {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                camEffects.Shake(0.1f);
                Destroy(collision.gameObject);
                anim.SetBool("Destroy", true);
                Destroy(gameObject, 0.5f);

            } else {
                anim.SetBool("isHit", true);
                Destroy(collision.gameObject, 0.25f);
                Destroy(gameObject, 1.5f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -0.5f);
            }


        }

    }
    // Use this for initialization
    void Awake() {
        anim = GetComponent<Animator>();
        //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -1);
        active = true;
        needsReset = true;
        player = GameObject.Find("Truck");
        camEffects = Camera.main.GetComponent<CameraEffects>();
    }

    // Update is called once per frame
    void Update() {
        TruckScript ts = player.GetComponent<TruckScript>();
        bool isReset = ts.reset;
        if (isReset && needsReset && active) {
            transform.Translate(0, -15, 0);
            needsReset = false;
        }
    }
}
