using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f; //f is zodat het een float is, anders is het een double en dat neemt meer ruimte op.

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        //starts when the game starts,
        // locking the cursor to the middle of the screen making it invisible 
        //maakt muis onzichtbaar anders zie je muis in je player  view
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //je doet het * mouseSensitivity omdat het anders heel traag is en...
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // de deltaTime zorgt er voor dat je niet sneller gaat als je hogere fps hebt omdat update() per frame wordt gerund dus heb je 60 fps wordt het 60 keer gerundt maar zou je ook sneller zijn dan 30 fps. deltaTime voorkomt dat.

        //omhoog en omlaag kijken is xRotation - y van je muis | 
        xRotation -= mouseY;
        
        //we clampen de x rotatie omdat je in het echt ook niet je neck helemaal 360 graden kan omhoog draaien, alsof je een handstand doet ofz dat kan not...
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 
        
        yRotation += mouseX; // als je dit omdraait kan je je muis naar links bewegen en dan kijk je naar rechts

        //pas beiden rotatie's toe op de camera
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
