using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;
    public static AudioManager audioManager;
    

    // Start is called before the first frame update
    void Start()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        audioManager.Play("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenScene() {
        SceneManager.LoadScene(sceneToLoad);
        audioManager.Pause("Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
