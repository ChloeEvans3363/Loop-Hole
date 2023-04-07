using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager Instance { get; private set; }
    public static AudioManager audioManager;
    public float fallSpeed = 5;
    public float fallIncrease = 0.8f;
    public float currentFallSpeed;
    public float previousFallSpeed = 0;
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
    private int gems;
    private float iTime;
    public bool dead = false;
    private float tutorialEndTime = 5.5f;

    private int highScore;
    private int totalGems;
    //Events and delegates
    public delegate void DamageAction();
    public static event DamageAction OnDamage;
    public Player playerScript;
    public Sprite normalSprite;
    public Sprite damagedSprite;
    public ParticleSystem particles;
    private float speedCap = 20;

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

    public int Gem
    {
        get { return gems; }
        set { gems = value; }
    }

    public int HighScore
    {
        get { return highScore; }
    }

    public float SpeedCap
    {
        get { return speedCap; }
        set { speedCap = value; }
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

    public void LoadData(GameData data)
    {
        this.highScore = data.highScore;
        this.totalGems = data.totalGems;

    }

    public void SaveData(ref GameData data)
    {
        data.highScore = this.highScore;
        data.totalGems = this.totalGems;
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

        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.sceneUnloaded += UnloadAssistant;
        OnDamage += HitParticles;
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
            if (fallSpeed < speedCap && fallSpeed >= currentFallSpeed)
            {
                fallSpeed += 2 * Time.deltaTime;
                currentFallSpeed = fallSpeed;
            }
            else if (fallSpeed < currentFallSpeed)
            {
                fallSpeed += 10 * Time.deltaTime;
            }
            if (fallSpeed > previousFallSpeed)
            {
                previousFallSpeed = fallSpeed;
            }
            score += (fallSpeed + Time.deltaTime) / 1000;
            //timeElapsed += Time.deltaTime;
            if (iTime > 0f)
            {
                iTime -= Time.deltaTime;
            }
            else{
                playerScript.GetComponent<SpriteRenderer>().sprite = normalSprite;
            }

            depth += fallSpeed * Time.deltaTime;

            if (stage == 3)
            {
                
            }

        }

        
        //Debug.Log("Score: " + score);

        //Death
        if(health == 0)
        {
            Time.timeScale = 0;
            if(depth > highScore)
            {
                highScore = Mathf.RoundToInt(depth);
            }
            if (!dead)
            {
                gems = Mathf.RoundToInt(depth) / 100;
                totalGems += gems;
                DataPersistenceManager.instance.SaveGame();
            }
            dead = true;
            audioManager.Pause("Theme");
        }

    }

    /// <summary>
    /// Take damage by amt
    /// </summary>
    /// <param name="amt"></param>
    public void TakeDamage(int amt)
    {
        
        if(iTime <= 0f && health != 0 && !tutorial)
        {
            iTime = 1.5f;
            health -= amt;
            playerScript.GetComponent<SpriteRenderer>().sprite = damagedSprite;
            if(fallSpeed - 10 <= 0)
            {
                fallSpeed = 5;
            }
            else
            {
                fallSpeed -= 10;
            }
            audioManager.Play("Hit");
            audioManager.Play("Pain");
            OnDamage();
        }
        
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

            if (fallSpeed < 15 && fallSpeed >= currentFallSpeed)
            {
                fallSpeed += 2 * Time.deltaTime;
                currentFallSpeed = fallSpeed;
            }
            else if (fallSpeed < currentFallSpeed)
            {
                fallSpeed += 10 * Time.deltaTime;
            }
            if (fallSpeed > previousFallSpeed)
            {
                previousFallSpeed = fallSpeed;
            }

            if (tutorialEndTime <= 0)
            {
                tutorialText.gameObject.SetActive(false);
                stage++;
                // Switch stages
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                tutorial = false;
            }
            tutorialEndTime -= Time.deltaTime;
        }
    }

    public void UnloadAssistant(Scene current)
    {
        OnDamage = null;
    }

    public Vector3 ScreenToWorld(float x, float y)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
    }

    public void Heal(){
        if (health < 3 && health > 0)
        {
            health++;
        }
    }

    public void HitParticles()
    {
        if(particles != null)
        {
            particles.Clear();
            particles.Play();
        }
    }
}
