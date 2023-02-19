using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    Vector2 position = new Vector2(0f, 0f);

    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        position = Vector2.Lerp(transform.position, new Vector2(mousePosition.x, transform.position.y), moveSpeed);

        transform.position = position;

    }
}
