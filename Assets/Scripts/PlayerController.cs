using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public CharacterController controller;

    public float speed = 10f;
    public float jumpHeight = 5f;

    //gravity stuff
    Vector3 velocity;
    public float gravity = -9.81f;

    //ground checking stuff
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    bool isGrounded;

    //shooting stuff
    public ParticleSystem muzzleFlash;
    [SerializeField] AudioClip fireSound;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

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

        //fire
        if (Input.GetMouseButtonDown(0))
        {
            muzzleFlash.Play();
            AudioHelper.PlayClip2D(fireSound, 100);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
