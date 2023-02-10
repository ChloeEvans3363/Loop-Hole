using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        ResetPosition();
    }

    void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Vector3 topBorder = ScreenToWorld(0, Screen.height);
        transform.Translate(Vector3.up * Time.deltaTime * gameManager.GetComponent<GameManager>().fallSpeed);


        if (transform.position.y - renderer.size.y > topBorder.y)
        {
            ResetPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hit");
            //Time.timeScale = 0;
            gameManager.GetComponent<GameManager>().fallSpeed = -3f;
            ResetPosition();
        }
    }

    private Vector3 ScreenToWorld(float x, float y)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
    }

    private void ResetPosition()
    {
        float newXPos = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + transform.localScale.x / 2, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - transform.localScale.x / 2);
        float newYPos = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, -Screen.height)).y);
        transform.position = new Vector3(newXPos, newYPos, 0);
        GetComponent<SpriteRenderer>().color = Color.green;
    }
}
