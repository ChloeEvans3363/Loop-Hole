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
    public Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    private Vector3 spriteSize;

    //Mobile stuff
    private Vector2 startPosition;

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

        if (Time.timeScale != 0)
        {
            /*
            mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if ((int)mousePosition.x > (int)transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(-spriteSize.x, spriteSize.y, spriteSize.z);
            }
            else if ((int)mousePosition.x < (int)transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(spriteSize.x, spriteSize.y, spriteSize.z);
            }

            transform.position = new Vector2(mousePosition.x, transform.position.y);
            */

            // Testing mobile movement
            //Debug.Log(inputManager.StartPosition);
            //Debug.Log(inputManager.PrimaryPosition());
            //Debug.Log(transform.position.x);
            //transform.position = new Vector2(inputManager.PrimaryPosition().x, transform.position.y);
            //Debug.Log(transform.position.x + (inputManager.StartPosition.x - inputManager.PrimaryPosition().x));
            float change = 0;
            //Debug.Log("huh " + transform.position);
            if (!inputManager.Touching)
            {
                //Debug.Log("change " + transform.position);
                startPosition = transform.position;
            }
            else
            {
                change = inputManager.StartPosition.x - inputManager.PrimaryPosition().x;
            }
            //Debug.Log("start " + startPosition);
            //Debug.Log(startPosition.x - change);
            //Debug.Log(inputManager.StartPosition.x + "+" + inputManager.PrimaryPosition().x + "=" + (inputManager.StartPosition.x - inputManager.PrimaryPosition().x));
            //Debug.Log("=" + change);
            transform.position = new Vector2(inputManager.StartPosition.x, transform.position.y);
        }

    }

    public void OnPause()
    {
        gameManager.isPaused = !gameManager.isPaused;
    }
}
