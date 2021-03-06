﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour {

    public Transform target;

    float shake;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 2.0f;

    public void Shake(float shakeAmount) {
        shake = shakeAmount;
    }

    public void Zoom() {
        StartCoroutine(ZoomIn());
    }

    void Update() {
        if (shake > 0) {
            Vector3 pos = Random.insideUnitSphere * shake;
            transform.position += new Vector3(pos.x, pos.y, 0);
            shake -= Time.deltaTime * decreaseFactor;
        } else {
            shake = 0;
        }

        
    }

    IEnumerator ZoomIn() {
        float currentRotationAngle = transform.eulerAngles.y;
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);



        float originalCamSize = Camera.main.orthographicSize;
        //Camera.main.orthographicSize = 1;
        /*while (Camera.main.orthographicSize > 1.1f) {
            yield return new WaitForSeconds(0.15f);
            
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * 5f;
            transform.LookAt(target.transform);
        }*/
        Camera.main.orthographicSize = 2.5f;
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * 5f;
        transform.LookAt(target.transform);
        yield return new WaitForSeconds(0.1f);
        
    }
}
