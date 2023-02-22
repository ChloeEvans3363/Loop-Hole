using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float fallSpeed = 5;
    public float fallIncrease = 0.8f;
    public float currentFallSpeed;
    public int stage;
    public bool isPaused = false;
    public HUDManager hudScript;

    [SerializeField] private bool tutorial;
    [SerializeField] private Text tutorialText;

    //Stuff from the game score manager. Don't wanna mess up what's already there but moving it here anyway.
    private int coins;
    private int health;
    private float score;
    private float depth;
    private float iTime;
    public bool dead = false;
    public GameObject objectManager;
    private float tutorialEndTime = 5.5f;
    //Events and delegates
    public delegate void DamageAction();
    public static event DamageAction OnDamage;

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
        iTime = 0f;
        stage = 1;
        if (!tutorial)
        {
            stage = 3;
            tutorialEndTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            hudScript.pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }

        else {
            hudScript.pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }

        if (tutorial)
        {
            Tutorial(stage);
        }
        else
        {
            if (fallSpeed < 15 && fallSpeed >= currentFallSpeed)
            {
                fallSpeed += 2 * Time.deltaTime;
                currentFallSpeed = fallSpeed;
            }
            else if (fallSpeed < currentFallSpeed)
            {
                fallSpeed += 10 * Time.deltaTime;
            }
            score += (fallSpeed + Time.deltaTime) / 1000;
            //timeElapsed += Time.deltaTime;
            if (iTime > 0f)
            {
                iTime -= Time.deltaTime;
            }

            depth += fallSpeed * Time.deltaTime;

            if (stage == 3)
            {
                if (tutorialEndTime <= 0)
                {
                    tutorialText.gameObject.SetActive(false);
                    objectManager.SetActive(true);
                    stage++;
                    
                }
                tutorialEndTime -= Time.deltaTime;
            }

        }

        
        //Debug.Log("Score: " + score);

        //Death
        if(health == 0)
        {
            Time.timeScale = 0;
            dead = true;
        }

        Debug.Log(stage);
    }

    /// <summary>
    /// Take damage by amt
    /// </summary>
    /// <param name="amt"></param>
    public void TakeDamage(int amt)
    {
        if(iTime <= 0f && health != 0 && !tutorial)
        {
            iTime = 0.5f;
            health -= amt;
        }
        OnDamage();
    }

    private void Tutorial(int stage)
    {
        if (stage == 2)
        {
            tutorialText.text = "You can jump on enemies heads to get more points. But if you hit them on the side you will take damage";
        }
        if(stage == 3)
        {
            tutorialText.text = "Alright, one last tip, you can hug the walls to slow down your speed! You're good to go! Good luck!";
            tutorial = false;
        }
    }
}
