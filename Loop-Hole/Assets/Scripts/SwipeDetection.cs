using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = .9f;

    public static InputManager inputManager;

    //Mobile stuff
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    void Awake()
    {
        if (inputManager == null)
        {
            inputManager = FindObjectOfType<InputManager>();
        }
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        Debug.Log("swipe detected");
        Vector3 direction = endPosition - startPosition;
        Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
        SwipeDirection(direction2D);
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("Swipe Right");
        }

        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("Swipe Left");
        }
    }

}
