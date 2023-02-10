using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float fallSpeed = 3;
    public float fallIncrease = 0.8f;
    public float currentFallSpeed;

    //Stuff from the game score manager. Don't wanna mess up what's already there but moving it here anyway.
    //Also why do the obstacles and walls have separate fall speeds? That doesn't make much sense.
    private float timeElapsed;
    [SerializeField] private float initialFallSpeed;
    [SerializeField] private float fallIncreaseRate;
    [SerializeField] private float fallSpeedCap;
    private int coins;
    private int health;
    private float score;
    private float depth;
    private float iTime;



    public float Depth
    {
        get { return depth; }
        set { depth = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    //End of game score manager

    void Awake()
    {
        if (Instance != null) 
            Destroy(Instance);
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }


    //Stuff in start and update is also from the game score manager
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        score = 0;
        fallSpeed = 3;
        iTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (fallSpeed < 15 && fallSpeed >= currentFallSpeed)
        {
            fallSpeed += 2 * Time.deltaTime;
            currentFallSpeed = fallSpeed;
            //Debug.Log("bye");
        }
        else if(fallSpeed < currentFallSpeed)
        {
            fallSpeed += 10 * Time.deltaTime;
            //Debug.Log("hi");
        }
        score += (fallSpeed + Time.deltaTime) / 1000;
        timeElapsed += Time.deltaTime;
        if(iTime > 0f)
        {
            iTime -= Time.deltaTime;
        }
        Debug.Log(fallSpeed);

        depth += fallSpeed * Time.deltaTime;
        //Debug.Log("Score: " + score);
    }

    /// <summary>
    /// Take damage by amt
    /// </summary>
    /// <param name="amt"></param>
    public void TakeDamage(int amt)
    {
        if(iTime <= 0f)
        {
            iTime = 0.5f;
            health -= amt;
        }
    }
}
