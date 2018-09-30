using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L8ESpawner : MonoBehaviour {

    public GameObject enemy;
    private GameObject[] numberOfEnemies;
    public int maxEnemies = 1;
    Vector2 whereToSpawn;  

	// Use this for initialization
	void Start () {
        whereToSpawn = new Vector2(gameObject.transform.position.x +0.1f, gameObject.transform.position.y -0.1f);
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

    // Update is called once per frame
    void Update() {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if ((numberOfEnemies.Length < maxEnemies) && (GameObject.Find("RoomManager").GetComponent<RML8>().roomComplete == false)) {
            //GameObject cretin = 
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
        }
    }
}
