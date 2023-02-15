using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        ResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 topBorder = ScreenToWorld(0, Screen.height);
        transform.Translate(Vector3.up * Time.deltaTime * gameManager.GetComponent<GameManager>().fallSpeed);
        
        if (transform.position.y > topBorder.y)
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
        //float newXPos = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + transform.localScale.x/2, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - transform.localScale.x/2);
        float newYPos = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, -Screen.height)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, -Screen.height * 2)).y);
        transform.position = new Vector3(transform.position.x, newYPos, 0);
        // Debug.Log(ScreenToWorld(newXPos, newYPos));
        //GetComponent<SpriteRenderer>().color = Color.red;
    }
}
