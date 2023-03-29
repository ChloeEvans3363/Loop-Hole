using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> leftWalls;
    [SerializeField] private List<GameObject> rightWalls;
    public GameManager gameManager;
    private int topWall = 0;
    private float wallHeight;

    // Start is called before the first frame update
    void Start()
    {
        if(leftWalls.Count > 0 && rightWalls.Count > 0)
        {
            wallHeight = leftWalls[0].GetComponent<SpriteRenderer>().bounds.size.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveWalls(leftWalls);
        MoveWalls(rightWalls);
    }

    private void MoveWalls(List<GameObject> wallList)
    {
        for (int i = 0; i < wallList.Count; i++)
        {
            if(i != topWall)
            {
                //Trying to figure out the math for this
                wallList[i].transform.position = new Vector2(wallList[i].transform.position.x, wallList[topWall].transform.position.y - wallHeight * i); 
            }
            else
            {
                wallList[topWall].transform.Translate(0f, gameManager.fallSpeed * Time.deltaTime, 0f);
            }
        }
        //Reset wall position
        if (wallList[topWall].transform.position.y > wallHeight)
        {
            topWall = (topWall+1)%wallList.Count;
        }
    }
}
