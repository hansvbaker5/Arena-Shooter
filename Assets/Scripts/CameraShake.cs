using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Camera mainCam;

    private float shakeAmount = 0;

    private void Update()
    {
        if (GameObject.Find("Main Camera") != null)
        {
            mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
    }

    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    private void BeginShake()
    {
        if(shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;

            float shakeAmountX = Random.value * shakeAmount * 2 - shakeAmount;
            float shakeAmountY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += shakeAmountX;
            camPos.y += shakeAmountY;

            mainCam.transform.position = camPos;
        }
    }

    private void StopShake()
    {
        CancelInvoke("BeginShake");
        mainCam.transform.position = new Vector3(0f, 0f, -1f);
    }

}
