using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = .9f;

    public static GameManager gameManager;
    public static InputManager inputManager;
    public float moveSpeed = 0.1f;
    private Vector3 spriteSize;
    private Vector2 controlPosition;

    //Mobile stuff
    private Vector2 startPosition;
    private Vector2 fingerStartPos;

    // Start is called before the first frame update

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if(inputManager == null)
        {
            inputManager = FindObjectOfType<InputManager>();
        }

        spriteSize = transform.localScale;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.playerInput.Touch.PrimaryContact.triggered && inputManager.playerInput.Touch.PrimaryContact.ReadValue<float>() == 1)
        {
            //Debug.Log("press");
            fingerStartPos = inputManager.PrimaryPosition();
        }

        if (Time.timeScale != 0)
        {

            if (Mouse.current != null)
            {
                controlPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            }
            else
            {
                float change = 0;
                if (!inputManager.Touching)
                {
                    startPosition = transform.position;
                }
                else
                {
                    change = fingerStartPos.x - inputManager.PrimaryPosition().x;
                }
                controlPosition = new Vector2(startPosition.x - change, transform.position.y);
            }
            

            if ((int)controlPosition.x > (int)transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(-spriteSize.x, spriteSize.y, spriteSize.z);
            }
            else if ((int)controlPosition.x < (int)transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(spriteSize.x, spriteSize.y, spriteSize.z);
            }

            transform.position = new Vector2(controlPosition.x, transform.position.y);
            

            // Testing mobile movement

            transform.position = new Vector2(controlPosition.x, transform.position.y);
        }

    }

    public void OnPause()
    {
        gameManager.isPaused = !gameManager.isPaused;
    }
}
