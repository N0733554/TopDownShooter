using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 mousePos;

    void Awake()
    {
        movement = new Vector2(0, 0);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
        movement = new Vector2(0, 0);

        Vector2 dir = mousePos - rb.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
