using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RMARENA : MonoBehaviour {

    public GameObject[] switches;
    public GameObject[] doors;
    //public int[] doorTimers = new int[] {600, 600, 600, 600, 600, 600, 600, 600, };
    public GameObject[] spawners;
    public GameObject exitDoor;
    private GameObject[] numberOfEnemies;
    public int maxActiveEnemies = 3;
    public int switchesOn = 0;
    public int doorsOpen = 0;
    //public Text switchCount;
    public Animator anim;
    public bool roomComplete = false;
    public int score = 0;
    public int level = 1;


    public int SFXplayed = 1;
    public AudioClip noise1;
    public AudioClip noise2;

    public AudioSource audioS;


    public void PlayNoise(AudioClip noise) {
        audioS.PlayOneShot(noise);
    }

    void Start() {
        GetActiveSwitches();
        roomComplete = false;
        for (int i = 0; i < doors.Length; i++) {
            //doorTimers[i] = 120;
            }
        }

    public void ChooseRandomSpawn() {
        int rand = Random.Range(0, spawners.Length);
        spawners[rand].GetComponent<EnemySpawnerScript>().SpawnAggroEnemy();
    }
    /*public void CountdownDoorTimers() {
        for (int i = 0; i < doorsOpen; i++) {
            if (doorTimers[i] > 0) {
                doorTimers[i]--;
            }
        }
    }*/

    public void GetActiveSwitches() {
        switchesOn = 0;
        for (int i = 0; i < switches.Length; i++) {
            if (switches[i].GetComponent<Switch>().isOn == true) {
                switchesOn++;
                //doorTimers[i] = 120;
            }
            /*if (doorTimers[i] < 1) {
                switches[i].GetComponent<Switch>().isOn = false;
            }*/
        }
    }

    public void OpenDoors() {
        /*doorsOpen = switchesOn;
        for (int i = 0; i < doorsOpen; i++) {
            OpenDoor(doors[i]);
            doors[i].GetComponent<Collider2D>().enabled = false;
        }
        */
    }

    public void CloseDoors() {
        for (int i = 0; i < doors.Length; i++) {
            /*if (doorTimers[i] < 1) {
                CloseDoor(doors[i]);
                doors[i].GetComponent<Collider2D>().enabled = true;
            }*/
        }
    }

    public void OpenExitDoor() {
        anim.SetFloat("DoorState", 3);
    }

    public void OpenDoor(GameObject door) {
        door.GetComponent<Animator>().SetFloat("DoorState", 3);
        //set the door timer here somehow fuck i'm too tired to think.
    }

    /*public void UnlockExitDoor() {
        anim.SetFloat("DoorState", 2);
        Debug.Log("unlocking door");
    }

    public void UnlockDoor(GameObject door) {
        door.GetComponent<Animator>().SetFloat("DoorState", 2);
        Debug.Log("unlocking door");
    }*/

    public void CloseExitDoor() {
        anim.SetFloat("DoorState", 1);
        Debug.Log("closing door");
    }

    public void CloseDoor(GameObject door) {
        door.GetComponent<Animator>().SetFloat("DoorState", 1); ;
    }

    void Update() {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        maxActiveEnemies = level+1;
        if (numberOfEnemies.Length < maxActiveEnemies) {
            ChooseRandomSpawn();
            score++;
        }
        if (score > 5) {
            level++;
            score = 0;
        }
        if (level > 7) {
            level = 7;
        }
    }

}
