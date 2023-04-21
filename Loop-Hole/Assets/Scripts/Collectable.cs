using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectable : MonoBehaviour
{
    public static GameManager gameManager;
    public static AudioManager audioManager;
    public Vector3 spawnPos;
    public float spawnTimer = 0.5f;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        spawnPos = RandomVector3(Random.Range(0,180), 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 topBorder = gameManager.ScreenToWorld(0, Screen.height);
   
        transform.position -= (spawnPos - transform.position).normalized * 3 * Time.deltaTime;
        transform.Translate(Vector3.up * Time.deltaTime * gameManager.GetComponent<GameManager>().fallSpeed);

        spawnTimer -= Time.deltaTime;
        
        if (transform.position.y > topBorder.y)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && spawnTimer <= 0)
        {
            if (gameManager.Health == 3 && !gameManager.tutorial)
            {
                gameManager.Depth += 50;
            }
            gameManager.Heal();
            Debug.Log("Heal");
            Destroy(this.gameObject);
            audioManager.Play("Pickup");
        }
    }

    public Vector3 RandomVector3(float angle, float angleMin){
        float random = Random.value * angle + angleMin;
        return new Vector3(Mathf.Cos(random), Mathf.Sin(random), 0);
    }
}
