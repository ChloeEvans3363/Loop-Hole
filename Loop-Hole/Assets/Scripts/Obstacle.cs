using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private bool move;
    private GameObject wall;
    private float obstacleWidth;
    private float wallWidth;
    private float wallX;
    private float limit;
    private float speed = 7;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        wall = gameManager.leftWall;
        if (move)
        {
            obstacleWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            wallWidth = wall.transform.localScale.x;
            wallX = wall.transform.position.x;
            limit = wallX + wallWidth + obstacleWidth;
        }

        Debug.Log("start called");
    }

    void Update()
    {
        if (move)
        {
            Move();
            string animationName = "MovingObs";
            Animator anim = this.GetComponent<Animator>();
            anim.Play(animationName);
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
        if(Vector2.Distance(transform.position, new Vector2(limit, transform.position.y)) < 0.02f)
        {
            limit = limit * -1;
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(limit, transform.position.y), speed * Time.deltaTime);
    }
}
