using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public static GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Vector3 topBorder = ScreenToWorld(0, Screen.height);
        transform.Translate(Vector3.up * Time.deltaTime * gameManager.GetComponent<GameManager>().fallSpeed);
    
        if (transform.position.y - 5 > topBorder.y)
        {
            ResetPosition();
        }
    }

    private Vector3 ScreenToWorld(float x, float y)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(transform.position.x, -20, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && gameManager.previousFallSpeed <= gameManager.fallSpeed)
        {
            gameManager.fallSpeed = gameManager.fallSpeed / 10;
        }
        
    }
}
