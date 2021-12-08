using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 movementVector;
    private Animator animator;
    private float speed;
    private Rigidbody body;
    private Health health; //New health variable


    
    void Start()
    {
        health = GetComponent<Health>(); // Get the health component
        animator = transform.GetChild(0).GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        speed = 1.5f;
    }


    void CalculateMovement()
    {
        movementVector = new Vector3(Input.GetAxis("Horizontal"), body.velocity.y, Input.GetAxis("Vertical"));

        body.velocity = new Vector3(movementVector.x * speed, movementVector.y, movementVector.z * speed);
    }


    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (movementVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementVector), 0.25f);
        }

        animator.SetBool("Walking", movementVector != Vector3.zero);

        if (Input.GetKeyDown(KeyCode.K))
        {
            Hurt(2, 2);
        }
    }

    public void Hurt(int amount, int delay = 0)
    {
        StartCoroutine(health.TakeDamageDelayed(amount, delay));
    }
}
