using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour {

    public Transform target;

    // shake effect taken from a post by poemdexter on the somethingawful.com forums
    float shake;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

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

        //float currentRotationAngle = transform.eulerAngles.y;
        //var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        //transform.position = target.position;
        //transform.position -= currentRotation * Vector3.forward * 5f;
        //transform.LookAt(target.transform);
    }

    IEnumerator ZoomIn() {
        float originalCamSize = Camera.main.orthographicSize;
        Camera.main.orthographicSize = 1;
        while (Camera.main.orthographicSize < originalCamSize) {
            yield return new WaitForSeconds(0.01f);
            Camera.main.orthographicSize += 0.1f;
        }
    }
}
