using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float moveSpeed = 5; 
    private Vector2 currentPosition; 
    private Stack<Vector2> positionHistory = new Stack<Vector2>(); 

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        Vector2 moveInput = Vector2.zero; 

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveInput = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveInput = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveInput = Vector2.left; 
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveInput = Vector2.right;
        }

        if (moveInput != Vector2.zero)
        {
            positionHistory.Push(currentPosition);

            currentPosition = new Vector2(Mathf.Clamp(currentPosition.x + moveInput.x, 0, 5), Mathf.Clamp(currentPosition.y + moveInput.y, 0, 5));

            transform.position = Vector2.MoveTowards(transform.position, currentPosition, moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Z) && positionHistory.Count > 0)
        {
            currentPosition = positionHistory.Pop();
            transform.position = currentPosition;
        }
    }
}
