using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 target;
    Vector2 mousePos;
    List<ZombieScript> neighbours;

    Vector2 accelVector;
    Vector2 movementVector;

    public Transform player;

    public float moveSpeed = 1f;
    public float turnSpeed = 2f;
    public float detectionRadius = 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        neighbours = new List<ZombieScript>();
        accelVector = new Vector2(0, 0);
        movementVector = new Vector2(0, 0);
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target = player.position;
    }

    void FixedUpdate()
    {
        /*
        // Rotate to look at the target
        Vector2 dir = target - rb.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        // Move towards target
        Vector2 moveVec = dir.normalized * moveSpeed;
        rb.MovePosition(rb.position + moveVec * Time.fixedDeltaTime);
        */

        // Acceleration is equal to flocking direction
        accelVector = Seek();

        // Add ecceleration onto the movement vector
        movementVector += accelVector * Time.fixedDeltaTime;
        // Essentially "clamp" movespeed to the max, keeps speed consistent
        movementVector.Normalize();
        movementVector *= moveSpeed;

        // Face sprite towards direction of movement
        float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        // Move position by velocity
        rb.MovePosition(rb.position + (movementVector * Time.fixedDeltaTime));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" && other != GetComponent<Collider2D>())
        {
            ZombieScript z = other.GetComponent<ZombieScript>();
            if (!neighbours.Contains(z))
            {
                Debug.Log(gameObject.name + " Found " + other.name);
                neighbours.Add(z);
            }            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            ZombieScript z = other.GetComponent<ZombieScript>();
            if (neighbours.Contains(z))
            {
                neighbours.Remove(z);
            }
        }
    }

    Vector2 Seek()
    {
        Vector2 desired = new Vector2(0, 0);

        desired = target - rb.position;
        desired.Normalize();
        desired *= turnSpeed;

        return desired;
    }
}
