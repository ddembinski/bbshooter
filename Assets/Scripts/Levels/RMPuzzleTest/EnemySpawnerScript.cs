using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {

    public GameObject enemy;
    private GameObject[] numberOfEnemies;
    public int maxEnemies = 1;
    Vector2 whereToSpawn;
    public GameObject initialTarget;
    private Vector2 initialTargetVector;

    public void SpawnAggroEnemy() {
        GameObject cretin = Instantiate(enemy, whereToSpawn, Quaternion.identity);
        cretin.GetComponent<RoboGeorgeScript>().originalPosition = initialTargetVector;
        cretin.GetComponent<RoboGeorgeScript>().atOrigin = false;
    }

    public void SpawnPeacefulEnemy() {
        GameObject cretin = Instantiate(enemy, whereToSpawn, Quaternion.identity);
        cretin.GetComponent<RoboGeorgeScript>().atOrigin = true;
    }

    // Use this for initialization
    void Start () {
        whereToSpawn = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        initialTargetVector = new Vector2(initialTarget.transform.position.x, initialTarget.transform.position.y);
    }

    // Update is called once per frame
    void Update() {

    }
}
