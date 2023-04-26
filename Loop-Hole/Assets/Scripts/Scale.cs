using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
        mainCamera = Camera.main;
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);
        // Problem area
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(Vector3.zero) * 100;
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(mainCamera.rect.width, mainCamera.rect.height)) * 100;
        
        Vector3 screenSize = topRight - bottomLeft;
        float screenRatio = screenSize.x / screenSize.y;
        float desiredRatio = transform.GetComponent<BoxCollider2D>().bounds.size.x / transform.GetComponent<BoxCollider2D>().bounds.size.y;

        //Debug.Log("before " + bottomLeft);
        //Debug.Log("after " + transform.TransformVector(transform.localScale));
        //Debug.Log(mainCamera.ViewportToWorldPoint(new Vector3(mainCamera.rect.width, mainCamera.rect.height)));

        if(screenRatio > desiredRatio)
        {
            float height = screenSize.y;
            //transform.localScale = new Vector3(height * desiredRatio, height);
            //transform.InverseTransformVector(new Vector3(height * desiredRatio, height));
            //Debug.Log(transform.InverseTransformVector(new Vector3(height * desiredRatio, height)));
        }
        else
        {
            float width = screenSize.x;
            //transform.localScale = new Vector3(width, width / desiredRatio);
            //transform.InverseTransformVector(new Vector3(width, width / desiredRatio));
            //Debug.Log(transform.InverseTransformVector(new Vector3(width, width / desiredRatio)));
        }

        //Debug.Log(desiredRatio);
        //transform.localScale = new Vector3(Screen.width/100, Screen.height/100, 0);

        //Debug.Log("width: " + Screen.width + " height: " + Screen.height);
        //var width = Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height;
        //transform.localScale = new Vector3((float)(width), (float)(width), (float)(width));
        //Debug.Log(transform.GetComponent<BoxCollider2D>().bounds.size);
        //Debug.Log(transform.GetComponent<BoxCollider2D>().bounds.size);
        //Debug.Log(transform.InverseTransformVector(transform.position));
        //Debug.Log((Camera.main.orthographicSize*2) * (Screen.width/Screen.height));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
