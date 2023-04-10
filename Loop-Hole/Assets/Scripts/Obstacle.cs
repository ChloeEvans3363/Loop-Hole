using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private bool move;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
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

    }
}
