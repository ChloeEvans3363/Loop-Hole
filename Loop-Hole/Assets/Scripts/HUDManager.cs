using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUDManager : MonoBehaviour
{
    //[SerializeField] private Text healthText;
    [SerializeField] private Text depthText;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<Image> healthIcons;
    [SerializeField] private Image speedNeedle;
    public static AudioManager audioManager;
    private int score;
    
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SetHealthText(gameManager.Health);
        SetDepthText(Mathf.RoundToInt(gameManager.Depth));
        UpdateHealthIcons(gameManager.Health);

        if (gameManager.dead)
        {
            gameOver.gameObject.SetActive(true);
            gameOver.gameObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Depth: " + score;
            gameOver.gameObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "High Score: " + gameManager.HighScore;
        }

        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        UpdateSpeedIcon();
    }

    public void Continue()
    {
        gameManager.isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
        audioManager.Pause("Theme");
    }

    //Set the health text
    /*public void SetHealthText(int num)
    {
        healthText.text = "Health: " + num + "/3";
    }*/

    //Set depth text
    public void SetDepthText(int num)
    {
        depthText.text = "Depth: " + num;
        score = num;
    }

    public void UpdateHealthIcons(int currentHealth)
    {
        for(int i = 0; i < healthIcons.Count; i++)
        {
            if(i < currentHealth)
            {
                healthIcons[i].color = Color.yellow;
            } 
            else
            {
                healthIcons[i].color = Color.gray;
            }
        }
    }

    public void UpdateSpeedIcon()
    {
        float angle = 180f;
        if (gameManager.fallSpeed > 0f)
        {
            angle = 180f - gameManager.fallSpeed / gameManager.SpeedCap * 180f;
        }
        speedNeedle.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
