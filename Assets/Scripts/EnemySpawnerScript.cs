using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {

    public GameObject enemy;
    float randX;
    Vector2 whereToSpawn;
    public float spawnRate = 2.0f;
    float nextSpawn = 0.0f;

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update() {
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-2.15f, 2.15f);
            if (transform.position.y > 30) {
                whereToSpawn = new Vector2(randX, -10);
            } else {
                whereToSpawn = new Vector2(randX, transform.position.y);
            }
            GameObject cretin = Instantiate(enemy, whereToSpawn, Quaternion.identity);
            Destroy(cretin, 15f);
        }
    }
}
