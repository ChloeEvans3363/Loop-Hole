using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HowToPlay : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject mainPanel;
    public GameObject howToPlayFreckle1;
    public GameObject howToPlayMouseIcon;
    public GameObject howToPlayPage2;
    public GameObject howToPlayPage3;
    public GameObject pageNumbers;
    private int currentPage = 1;
    private int maxPages = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void ToggleHowToPlay(){
        howToPlayPanel.SetActive(!howToPlayPanel.active);
        mainPanel.SetActive(!mainPanel.active);
        Debug.Log("Before If Statement");
        if(!howToPlayPanel.active)
        {
            howToPlayFreckle1.SetActive(true);
            howToPlayMouseIcon.SetActive(true);
            howToPlayPage2.SetActive(false);
            howToPlayPage3.SetActive(false);

            currentPage = 1;

            string animationName = "HowToPlay" + currentPage;
            Animator anim = howToPlayPanel.GetComponent<Animator>();
            anim.Play(animationName);

            TextMeshProUGUI text = pageNumbers.GetComponent<TextMeshProUGUI>();
            text.text = currentPage + "/3";
            Debug.Log("In If Statement");
        }
    }

    public void SwitchPage(bool nextPage){
        if ((currentPage == maxPages && nextPage) || (currentPage == 1 && !nextPage))
        {
            return;
        }

        if(nextPage)
        {
            currentPage++;
        }
        else
        {
            currentPage--;
        }

        switch(currentPage)
        {
            case 1:
                howToPlayFreckle1.SetActive(true);
                howToPlayMouseIcon.SetActive(true);
                howToPlayPage2.SetActive(false);
                howToPlayPage3.SetActive(false);
                break;
            case 2:
                howToPlayFreckle1.SetActive(false);
                howToPlayMouseIcon.SetActive(false);
                howToPlayPage2.SetActive(true);
                howToPlayPage3.SetActive(false);
                break;
            case 3:
                howToPlayFreckle1.SetActive(false);
                howToPlayMouseIcon.SetActive(false);
                howToPlayPage2.SetActive(false);
                howToPlayPage3.SetActive(true);
                break;
        }
             
        string animationName = "HowToPlay" + currentPage;
        Animator anim = howToPlayPanel.GetComponent<Animator>();
        anim.Play(animationName);

        TextMeshProUGUI text = pageNumbers.GetComponent<TextMeshProUGUI>();
        text.text = currentPage + "/3";
    }
}


