using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float xAnchor;
    [SerializeField] private float acceleration = 0f;
    [SerializeField] private float startingShakeSpeed = 32f;
    [SerializeField] private float magnitude = 0.25f;
    [SerializeField] private float numberOfShakes = 4f;
    private bool shakeEnabled;

    // Start is called before the first frame update
    void Start()
    {
        xAnchor = transform.position.x;
        //StartCoroutine(Shake());
        GameManager.OnDamage += StartShake;
        shakeEnabled = PlayerPrefs.GetInt("screenshakeEnabled") != 0; //this doesn't seem to work. I don't know why!
    }

    public void StartShake()
    {
        if(shakeEnabled)
        StartCoroutine(Shake());
    }

    //Coroutine for screenshake
    public IEnumerator Shake()
    {
        //Set initial variables for screenshake
        float shakeSpeed = startingShakeSpeed;
        float offsetSource = 0f;
        float shakeLimit = Mathf.PI * numberOfShakes * 2f;
        //Loop each frame
        while (offsetSource <= shakeLimit)
        {
            shakeSpeed += acceleration;
            //Increase the offset of the screen
            offsetSource += shakeSpeed * Time.deltaTime;
            //Set the x position
            float x = xAnchor + Mathf.Sin(offsetSource) * magnitude;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            yield return null;
        }
    }
}
