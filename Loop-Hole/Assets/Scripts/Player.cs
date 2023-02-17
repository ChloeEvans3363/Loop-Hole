using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);
    Vector2 test = new Vector2(0f, 0f);

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        position = Vector2.Lerp(transform.position, new Vector2(mousePosition.x, 0), moveSpeed);
        //mousePosition.x = Mathf.Clamp(mousePosition.x, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x + transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x * -1 - transform.GetComponent<SpriteRenderer>().bounds.size.x / 2);
        test = new Vector2(mousePosition.x ,0);
        //Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x);
        //Debug.Log(mousePosition.x);
        //rb.MovePosition(test);
        //rb.AddForce(new Vector2(mousePosition.x, 0).normalized * 1);
        //rb.position = mousePosition;
        //transform.position = mousePosition;
        //Debug.Log(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}
