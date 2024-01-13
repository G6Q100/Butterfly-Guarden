using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 velocity;
    private bool grounded;
    private float speed = 5f;
    private float accelerateSpd = 15f;
    private float runSpd = 10f;
    private float normalSpd = 5f;
    private float gravity = -10f;
    Animator animator;

    public float canMove = -1;

    // camera
    public GameObject cameraFollow;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraFollow = GameObject.Find("CameraFollow");
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        canMove += Time.deltaTime;
        if (canMove > 0)
        {
            Movement();
            CameraFollow();
        }

        if(transform.position.y < -20)
        {
            transform.position = new Vector3(0, -3.5f, -5);
        }
    }

    void Movement()
    {
        grounded = characterController.isGrounded;
        if (grounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        // player movement
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontal, 0, vertical);
        var move = direction * speed * Time.deltaTime;
        characterController.Move(cameraFollow.transform.rotation * move);


        if (move != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed += accelerateSpd * Time.deltaTime;
                if(speed > runSpd)
                {
                    speed = runSpd;
                }

                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRunning", true);
            }
            else 
            {
                speed -= accelerateSpd * Time.deltaTime;
                if (speed < normalSpd)
                {
                    speed = normalSpd;
                }
                if (speed < runSpd)
                {
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsRunning", false);
                }
            }
            gameObject.transform.forward = cameraFollow.transform.rotation * move;
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalking", false);
        }
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void CameraFollow()
    {
        cameraFollow.transform.position = transform.position;
    }
}
