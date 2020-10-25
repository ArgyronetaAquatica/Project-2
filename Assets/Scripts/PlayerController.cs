using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public CharacterController controller;
    public Level01Controller levelController;

    [SerializeField] AudioClip damageSound = null;

    public float speed = 10f;
    public float jumpHeight = 5f;
    public int health = 100;
    public bool alive = true;

    //gravity stuff
    Vector3 velocity;
    public float gravity = -9.81f;

    //ground checking stuff
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (alive && !levelController.gameOver)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            //jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            //sprint
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed += 5;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed -= 5;
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

    }

    public void Damage(int dmgAmount)
    {
        health -= dmgAmount;
        AudioHelper.PlayClip2D(damageSound, 1f);
    }

}
