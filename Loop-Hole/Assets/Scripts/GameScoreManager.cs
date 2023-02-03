using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    private float timeElapsed;
    [SerializeField] private float initialFallSpeed;
    [SerializeField] private float fallIncreaseRate;
    [SerializeField] private float fallSpeedCap;
    private float fallSpeed;
    private int coins;
    private int health;

    public float FallSpeed
    {
        get { return fallSpeed; }
        set { fallSpeed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        //Debug.Log("Time: " + timeElapsed);
    }
}
