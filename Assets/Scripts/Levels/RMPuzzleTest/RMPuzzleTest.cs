using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RMPuzzleTest : MonoBehaviour {

    public GameObject[] switches;
    public GameObject[] doors;
    public GameObject exitDoor;
    public int switchesOn = 0;
    public int doorsOpen = 0;
    //public Text switchCount;
    public Animator anim;
    public bool roomComplete = false;

    void Start() {
        GetActiveSwitches();
        roomComplete = false;
    }

    public void GetActiveSwitches() {
        switchesOn = 0;
        for (int i = 0; i < switches.Length; i++) {
            if (switches[i].GetComponent<Switch>().isOn == true) {
                switchesOn++;
            } 
        }
    }

    public void OpenDoors() {
        doorsOpen = switchesOn;
        for (int i = 0; i < doorsOpen; i++) {
            OpenDoor(doors[i]);
            doors[i].GetComponent<Collider2D>().enabled = false;
        }
    }

    public void CloseDoors() {
        for (int i = 0; i < doors.Length; i++) {
            CloseDoor(doors[i]);
            doors[i].GetComponent<Collider2D>().enabled = true;
        }
    }

    public void OpenExitDoor() {
        anim.SetFloat("DoorState", 3);
    }

    public void OpenDoor(GameObject door) {
        door.GetComponent<Animator>().SetFloat("DoorState", 3);
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
        if (doorsOpen == doors.Length) {
            roomComplete = true;
        }
        if (!roomComplete) {
            GetActiveSwitches();
            CloseDoors();
            OpenDoors();
            if (switchesOn < switches.Length) {

            }

        }
    }

}
