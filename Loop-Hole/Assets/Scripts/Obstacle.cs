using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private bool move;
    [SerializeField] GameObject wall;
    private float obstacleWidth;
    private float wallWidth;
    private float wallX;
    private Vector2 limit;
    private float speed = 2;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if (move)
        {
            obstacleWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            wallWidth = wall.transform.localScale.x;
            wallX = wall.transform.position.x;
            limit = new Vector2(wallX  + wallWidth + obstacleWidth, transform.position.y);
        }
    }

    void Update()
    {
        if (move)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().TakeDamage(1);
        }
    }

    private void Move()
    {
        if(Vector2.Distance(transform.position, limit) < 0.02f)
        {
            limit = new Vector2(limit.x*-1, limit.y);
        }
        transform.position = Vector2.MoveTowards(transform.position, limit, speed * Time.deltaTime);
    }
}
