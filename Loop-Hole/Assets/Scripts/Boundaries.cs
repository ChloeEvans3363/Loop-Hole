using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    //private Vector2 screenBounds;
    private float playerWidth;
    public GameObject wall;
    private float wallWidth;
    private float wallX;

    // Start is called before the first frame update
    void Start()
    {
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        playerWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        //wallWidth = wall.transform.GetComponent<SpriteRenderer>().bounds.size.x;
        //wallWidth = 1.125f;
        wallWidth = transform.localScale.x;
        wallX = wall.transform.position.x;
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, wallX + playerWidth + wallWidth, wallX * -1 - playerWidth - wallWidth);
        transform.position = viewPos;
    }
}
