using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private DialogueSystem dialogueSystem;
    [SerializeField]
    private GameObject root;

    private Vector3 velocity;
    private bool grounded;
    private float speed = 3f;
    private float accelerateSpd = 15f;
    private float runSpd = 10f;
    private float normalSpd = 5f;
    private float gravity = -10f;
    Animator animator;





    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        if (GameObject.Find("Dialogue System") != null)
        {
            dialogueSystem = GameObject.Find("Dialogue System").GetComponent<DialogueSystem>();
            dialogueSystem.dialogueImage.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // player movement
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * -0.58f);
        if (EventSettings.instance.quest == 1 && dialogueSystem.talking != true &&
            Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) > 2)
        {
            Movement();
        }
        else
        {
            animator.SetBool("IsWalking",false);
            animator.SetBool("IsRunning", false);
        }
    }

    void Movement()
    {
        grounded = characterController.isGrounded;
        if (grounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        var move = Vector3.forward * speed * Time.deltaTime;
        //float move = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, move);
        characterController.Move(transform.rotation * move);

        if (move != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed += accelerateSpd * Time.deltaTime;
                if (speed > 0)
                {
                    speed = runSpd;
                    animator.SetBool("IsRunning", true);
                    animator.SetBool("IsWalking", false);
                }
            }
            else
            {
                speed -= accelerateSpd * Time.deltaTime;
                if (speed < 0)
                {
                    speed = normalSpd;
                    if (animator.GetBool("IsWalking") != true)
                    {
                        animator.SetBool("IsWalking", true);
                        animator.SetBool("IsRunning", false);
                    }
                }

            }
            //This code cause the npc cannot look at player
            //gameObject.transform.forward = move;
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalking", false);
        }
        velocity.y += gravity * Time.deltaTime;


        characterController.Move(velocity * Time.deltaTime);
    }
}
