using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserScript : MonoBehaviour
{
    public Rigidbody2D rb;

    List<Vector2> pathToTravel = new List<Vector2>();
    // Use this for initialization
    void Start()
    {

    }

    void Movement(){
        //lateral speed of player
        float vel = rb.velocity.x;

        bool moving = false;

        float ForceX = 12.5f;

        //if (turnsToMonster == false)
        //{
        //    transform.localScale = new Vector2(1, 0.8f);
        //}
        //else if (turnsToMonster == true)
        //{
        //    transform.localScale = new Vector2(-1, 0.8f);
        //}

        //checks if in motion
        if (vel > 0 || vel < 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        //sets the speed and acceleration based on direction and sensitivity of joystick
        //if (hInput < 0)
        //{
        //    if (hInput < -deadZoneThreshold)
        //    {
        //        if (hInput > -crawlThreshold)
        //        {
        //            speedX = -crawlSpeed;
        //            forceX = -crawlForce;
        //        }
        //        else if (hInput > -walkThreshold)
        //        {
        //            speedX = -walkSpeed;
        //            forceX = -walkForce;
        //        }
        //        else if (hInput < -walkThreshold)
        //        {
        //            speedX = -runSpeed;
        //            forceX = -runForce;
        //        }
        //    }
        //}
        //if (hInput > 0)
        //{
        //    if (hInput > deadZoneThreshold)
        //    {
        //        if (hInput < crawlThreshold)
        //        {
        //            speedX = crawlSpeed;
        //            forceX = crawlForce;
        //        }
        //        else if (hInput < walkThreshold)
        //        {
        //            speedX = walkSpeed;
        //            forceX = walkForce;
        //        }
        //        else if (hInput > walkThreshold)
        //        {
        //            speedX = runSpeed;
        //            forceX = runForce;
        //        }
        //    }
        //}




        //if (vel < 0)
        //{
        //    if (hInput > deadZoneThreshold)
        //    {
        //        rb.velocity = new Vector2(0, rb.velocity.y);
        //    }
        //}

        //if (vel > 0)
        //{
        //    if (hInput < -deadZoneThreshold)
        //    {
        //        rb.velocity = new Vector2(0, rb.velocity.y);
        //    }
        //}

        rb.AddForce(new Vector2(ForceX, rb.velocity.y));
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
