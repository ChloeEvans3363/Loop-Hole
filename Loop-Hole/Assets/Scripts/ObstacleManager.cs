using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static GameManager gameManager;
    public List<GameObject> listOfObstacles;

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
        Vector3 topBorder = gameManager.ScreenToWorld(0, Screen.height);
        transform.Translate(Vector3.up * Time.deltaTime * gameManager.GetComponent<GameManager>().fallSpeed);
        
        if (transform.position.y > topBorder.y)
        {
            ChangeObsLayout();
        }
    }
    

    private void ResetPosition()
    {
        float newYPos = Camera.main.ScreenToWorldPoint(new Vector2(0, -Screen.height * 1.5f)).y;
        transform.position = new Vector3(transform.position.x, newYPos, 0);
    }


    private void ChangeObsLayout()
    {
        int obsLayoutNum = Random.Range(0,listOfObstacles.Count);
        GameObject nextObs = listOfObstacles[obsLayoutNum];

        Instantiate(nextObs);
        Destroy(this.gameObject);
    }
}
