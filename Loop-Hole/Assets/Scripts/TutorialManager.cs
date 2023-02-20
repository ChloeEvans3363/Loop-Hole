using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
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
        Vector3 topBorder = ScreenToWorld(0, Screen.height);
        transform.Translate(Vector3.up * Time.deltaTime * gameManager.GetComponent<GameManager>().fallSpeed);

        if (transform.position.y > topBorder.y)
        {
            ChangeObsLayout(gameManager.GetComponent<GameManager>().stage - 1);
        }

        if(gameManager.GetComponent<GameManager>().stage == 3)
        {
            transform.gameObject.SetActive(false);
        }

    }

    private Vector3 ScreenToWorld(float x, float y)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
    }


    private void ResetPosition()
    {
        float newYPos = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, -Screen.height)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, -Screen.height * 2)).y);
        transform.position = new Vector3(transform.position.x, newYPos, 0);
    }


    private void ChangeObsLayout(int stage)
    {
        GameObject nextObs = listOfObstacles[stage];

        Instantiate(nextObs);
        Destroy(this.gameObject);
    }
}
