using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUDManager : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text depthText;
    [SerializeField] private Text gameOver;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<Image> healthIcons;
    
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthText(gameManager.Health);
        SetDepthText(Mathf.RoundToInt(gameManager.Depth));
        UpdateHealthIcons(gameManager.Health);

        if (gameManager.dead)
        {
            gameOver.gameObject.SetActive(true);
        }
    }

    public void Continue()
    {
        gameManager.isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    //Set the health text
    public void SetHealthText(int num)
    {
        healthText.text = "Health: " + num + "/3";
    }

    //Set depth text
    public void SetDepthText(int num)
    {
        depthText.text = "Depth: " + num;
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
}
