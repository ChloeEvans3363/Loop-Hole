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
    private Vector2 position;
    private Rigidbody2D rb;
    private bool isPaused;

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
        rb = GetComponent<Rigidbody2D>();
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale != 0)
        {

            if (Mouse.current != null)
            {
                controlPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            }     

            if ((int)controlPosition.x > (int)transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(-spriteSize.x, spriteSize.y, spriteSize.z);
            }
            else if ((int)controlPosition.x < (int)transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(spriteSize.x, spriteSize.y, spriteSize.z);
            }

            //transform.position = new Vector2(controlPosition.x, transform.position.y);

            position = new Vector2(controlPosition.x, transform.position.y);

            if (isPaused)
            {
                position = Vector2.Lerp(transform.position, position, 0.1f);
                if((int)controlPosition.x == (int)transform.position.x)
                {
                    isPaused = false;
                }
            }

        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }

    public void OnPause()
    {
        gameManager.isPaused = !gameManager.isPaused;
        isPaused = true;
    }
}
