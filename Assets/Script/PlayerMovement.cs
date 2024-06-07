using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2; //het is momenteel 2 omdat je anders in game langzaam valt, maar dat is gewoon voorkeur
    public float jumpHeight = 3f;

    public float flySpeed = 5f;  // Speed while flying
    public float flyGravity = -3f;  // Gravity while flying
    public float doubleTapTime = 0.5f;  // Time interval for double-tap detection
    float lastSpacePressTime;
    bool isFlying = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; // dit is voor het detecteren van de vloer of vloer mask

    Vector3 velocity; //hoe snel we vallen de velocity

    bool isGrounded; //is je poppentje op de grond

    void Update()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //reset als ware de snelheid van het vallen
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //dit berekend de jump.(moeilijk) hoef je niet te snappen wel leuk als ik het ooit snap
            print("normal jump");
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            if (Time.time - lastSpacePressTime < doubleTapTime)
            {
                isFlying = true;  // Toggle flying mode
                print("vliegen is true");
            }
            lastSpacePressTime = Time.time;
        }


        if (isFlying)
        {
            if (Input.GetButton("Jump"))
            {
                velocity.y = flySpeed;
                print("velocify.y" + velocity.y);
            }
            else
            {
                velocity.y = flyGravity;
                if (Input.GetButton("LeftControl"))
                {
                    velocity.y = flyGravity - 6f;
                }
            }
            if (isGrounded)
            {
                isFlying = false;

            }
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

