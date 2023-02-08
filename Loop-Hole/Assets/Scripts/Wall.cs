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
        transform.Translate(Vector3.up * Time.deltaTime * gameManager.GetComponent<GameManager>().wallFallSpeed);
        gameManager.GetComponent<GameManager>().wallFallSpeed += 1.0f * Time.deltaTime;
    
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
}
