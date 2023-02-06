using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float fallSpeed = 5.0f;


    void Start()
    {

    }

    void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Vector3 topBorder = ScreenToWorld(0, Screen.height);
        transform.Translate(Vector3.up * Time.deltaTime * fallSpeed);
        fallSpeed += 1.0f * Time.deltaTime;
    
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
