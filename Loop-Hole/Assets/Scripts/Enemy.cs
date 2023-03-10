using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject collectable;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().fallSpeed = -3f;
            Instantiate(collectable);
            collectable.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
}
