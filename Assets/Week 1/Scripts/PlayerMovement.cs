using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float strafeSpeed;
    [SerializeField]
    private float moveTime;
    private float moveSpeed;


    private Transform trans;
    private Rigidbody rb;

    [SerializeField]
    private Transform firstObjectPos;
    [SerializeField]
    private float jumpDistance;
    //private float jumpSpeed;

    private bool grounded;
    private bool playing;

    public int score;


    //private float xVel;

    private Vector3 Accel;
    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {


        if (Input.GetMouseButtonDown(0) && !playing)
        {
            /*
             // Velocity
            moveSpeed = (firstObjectPos.position.z - trans.position.z) / moveTime;
            InvokeRepeating("AddScore", 1.0f, 1.0f);
            */

            //Acceleration
            float distance = (firstObjectPos.position.z - trans.position.z);
            Accel = CalcAccel(moveTime, rb.velocity, distance) * Vector3.forward;
            Debug.Log("Acceleration: " + Accel.magnitude);
            playing = true;

            if (playing)
            {
                ///// CHANGE EVERYTHING FOR FORCE INSTEAD OF VELOCITY: Base it off of acceleration
                


                /*
                 // Velocity
                if (Input.GetKey(KeyCode.A))
                {
                    xVel = -strafeSpeed;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    xVel = strafeSpeed;
                }
                else
                {
                    xVel = 0;
                }

                if (Input.GetKeyDown(KeyCode.Space) && grounded)
                {
                    rb.velocity += new Vector3(0, jumpSpeed,0);
                }

                rb.velocity = new Vector3(xVel, rb.velocity.y, moveSpeed);
                */
            }
        }

    }
    private void FixedUpdate()
    {

        if (playing)
        {

            //Acceleration
            rb.AddForce(Accel, ForceMode.Acceleration);
        }
    }
    void AddScore()
    {
        score += 1;
        Debug.Log("Got 1 point");
    }



    float CalcAccel(float t, Vector3 vI, Vector3 vF)
    {
        return t > Mathf.Epsilon ? (vF.magnitude - vI.magnitude) / t : 0.0f;
    }

    float CalcAccel( float t, Vector3 vI, float d )
    {

        //(vF^2 - vI^2) / 2d

        if (t < Mathf.Epsilon)
        {
            return 0;
        }
        return ((d - (vI.magnitude * t)) * 2) / (t * t);
    }

    float CalcAccel(float t, Vector3 vI, Vector3 vF, float d)
    {
        if (t < Mathf.Epsilon)
        {
            return 0;
        }
        return (((vF.magnitude * vF.magnitude) - (vI.magnitude * vI.magnitude)) / 2.0f * d);
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor")
        {
            grounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Floor")
        {
            grounded = false;
        }
    }
}
