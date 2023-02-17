using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text depthText;
    [SerializeField] private Text gameOver;
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthText(gameManager.Health);
        SetDepthText(Mathf.RoundToInt(gameManager.Depth));

        if (gameManager.dead)
        {
            gameOver.gameObject.SetActive(true);
        }
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
}
