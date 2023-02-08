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
        transform.Translate(Vector3.up * Time.deltaTime * gameManager.GetComponent<GameManager>().obstacleFallSpeed);
        gameManager.GetComponent<GameManager>().obstacleFallSpeed += 0.8f * Time.deltaTime;

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
            gameManager.GetComponent<GameManager>().obstacleFallSpeed = -2.5f;
            gameManager.GetComponent<GameManager>().wallFallSpeed = -6;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    private Vector3 ScreenToWorld(float x, float y)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
    }

    private void ResetPosition()
    {
        Vector2 test = new Vector3(500, Random.Range(-Screen.height + GetComponent<SpriteRenderer>().size.y, 0 - GetComponent<SpriteRenderer>().size.y));
        transform.position = ScreenToWorld(test.x, test.y);
        //Debug.Log(ScreenToWorld(test.x, test.y));
        //float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        //float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + GetComponent<SpriteRenderer>().size.x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - GetComponent<SpriteRenderer>().size.x);
        //transform.position = new Vector2(spawnX, spawnY);
        GetComponent<SpriteRenderer>().color = Color.green;
    }
}
