using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static GameManager gameManager;
    public Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    // Start is called before the first frame update

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            transform.position = new Vector2(mousePosition.x, transform.position.y);
        }
    }

    public void OnPause()
    {
        gameManager.isPaused = !gameManager.isPaused;
    }
}
