using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> leftWalls;
    [SerializeField] private List<GameObject> rightWalls;
    private int[,] wallOffsets; //I should be using math for this instead but I just could not figure it out
    public GameManager gameManager;
    private int topWall = 0;
    private float wallHeight;
    [SerializeField] private float seamOverlap;

    // Start is called before the first frame update
    void Start()
    {
        if (leftWalls.Count > 0 && rightWalls.Count > 0)
        {
            wallHeight = leftWalls[0].GetComponent<SpriteRenderer>().bounds.size.y - seamOverlap;
            wallOffsets = 
                new int[,] { {0, 4, 3, 2, 1}, 
                             {1, 0, 4, 3, 2}, 
                             {2, 1, 0, 4, 3}, 
                             {3, 2, 1, 0, 4}, 
                             {4, 3, 2, 1, 0} };
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
                //I should absolutely be using math for this instead
                //it should always be 0 if i = topWall
                int wallOffsetMultiplier = wallOffsets[i, topWall];
                //Debug.Log(i + ", " + wallList.Count + ", " + topWall + " = " + wallOffsetMultiplier);
                wallList[i].transform.position = new Vector2(wallList[i].transform.position.x, wallList[topWall].transform.position.y - wallHeight * wallOffsetMultiplier); 
            }
            else
            {
                wallList[topWall].transform.Translate(0f, gameManager.fallSpeed * Time.deltaTime, 0f);
            }
        }
        //Reset wall position
        if (wallList[topWall].transform.position.y > wallHeight)
        {
            topWall++;
            if(topWall >= wallList.Count)
            {
                topWall = 0;
            }
        }
    }
}
