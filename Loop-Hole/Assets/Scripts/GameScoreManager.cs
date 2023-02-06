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
    private float score;

    public float FallSpeed
    {
        get { return fallSpeed; }
        set { fallSpeed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        score = 0;
        fallSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        fallSpeed += 0.2f * Time.deltaTime;
        score += (fallSpeed + Time.deltaTime) / 1000;
        timeElapsed += Time.deltaTime;

        Debug.Log("Score: " + score);
    }
}
