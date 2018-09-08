using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public int english = 0;
    Vector3 curveLeft = Vector3.left;
    Vector3 curveRight = Vector3.right;

    public bool needsReset = true;
    private GameObject player;

    void Awake () {
        if (english <= -1) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(curveLeft * 100);
        } else if (english >= 1) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(curveRight * 100);
        }
        needsReset = true;
        player = GameObject.Find("Truck");

    }

    // Update is called once per frame
    void Update () {
        TruckScript ts = player.GetComponent<TruckScript>();
        bool isReset = ts.reset;
        if (isReset && needsReset && (gameObject.tag != "DeadBall")) {
            transform.Translate(0, -15, 0);
            needsReset = false;
        }
    }
}
