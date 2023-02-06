using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float fallSpeed = 3;


    void Start()
    {
        ResetPosition();
    }

    void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Vector3 topBorder = ScreenToWorld(0, Screen.height);
        transform.Translate(Vector3.up * Time.deltaTime * fallSpeed);
        fallSpeed += 0.8f * Time.deltaTime;

        if (transform.position.y - renderer.size.y > topBorder.y)
        {
            ResetPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    private Vector3 ScreenToWorld(float x, float y)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
    }

    private void ResetPosition()
    {
        transform.position = ScreenToWorld(Random.Range(0 + GetComponent<SpriteRenderer>().size.x, Screen.width - GetComponent<SpriteRenderer>().size.x), Random.Range(-Screen.height + GetComponent<SpriteRenderer>().size.y, 0 - GetComponent<SpriteRenderer>().size.y));
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
