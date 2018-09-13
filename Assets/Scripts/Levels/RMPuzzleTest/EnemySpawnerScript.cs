using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {

    public GameObject enemy;
    private GameObject[] numberOfEnemies;
    public int maxEnemies = 1;
    Vector2 whereToSpawn;  

	// Use this for initialization
	void Start () {
        whereToSpawn = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

    // Update is called once per frame
    void Update() {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if ((numberOfEnemies.Length < 1) && (GameObject.Find("RoomManager").GetComponent<RMPuzzleTest>().roomComplete == false)) {
            GameObject cretin = Instantiate(enemy, whereToSpawn, Quaternion.identity);
        }
    }
}
