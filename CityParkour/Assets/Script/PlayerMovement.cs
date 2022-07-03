using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   Ray ray;
    public float range = 1f;
    public float climbspeed = 5f;
   // public float sticktowallforce = 5f;

    public float turnSmoothing = 15f; // A smoothing value for turning the player.
    public float speedDampTime = 0.1f; // The damping for the speed parameter
    public bool climb;
    public Vector3 jumping;
    public float jumpForce = 2.0f;
    Rigidbody rb;
    Vector3 movement;
    public bool isGrounded;

    private Animator anim; // Reference to the animator component.
    protected Joystick joystick;
    protected joybutton joybutton;
    public AudioClip jumpingClip;     

    void Awake()
    {
        // Setting up the references.
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<joybutton>();
        rb = GetComponent<Rigidbody>();
        jumping = new Vector3(0.0f, 2.0f, 0.0f);
        anim = GetComponent<Animator>();
        
        // Set the weight of the jumping layer to 1.
        anim.SetLayerWeight(1, 1f);
       
    }

    void FixedUpdate()
    {
        // Cache the inputs.

        float h = joystick.Horizontal * 100f;
        float v = joystick.Vertical * 100f;
        //rb.velocity = new Vector3( h , rb.velocity.y,  v );
        MovementManagement(h, v);
    }

    void Update()
    {

        bool jump = false;
        if (joybutton.Pressed)
        {
            jump = true;
        }
        if (!joybutton.Pressed)
        {
            jump = false;
        }

        if (joybutton.Pressed && isGrounded == true )
        {
            //rb.velocity += Vector3.up * 10f;
            rb.AddForce(jumping * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
     
        anim.SetBool("Jump", jump);
        AudioManagement(jump,climb);

    }

    void MovementManagement(float horizontal, float vertical)
    {
     
        


        // If there is some axis input...
        if (horizontal != 0f || vertical != 0f)
        {
            // ... set the players rotation and set the speed parameter to 5.5f.
            movement.Set(horizontal, 0f, vertical);


            movement = movement.normalized * 5f * Time.deltaTime;

            rb.MovePosition(transform.position + movement);

            Rotating(horizontal, vertical);
            anim.SetFloat("Speed", 5.5f);



        }
        else
        {
            // Otherwise set the speed parameter to 0.
            anim.SetFloat("Speed", 0f);

        }

        
    }

    void Rotating(float horizontal, float vertical)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
        // Change the players rotation to this new rotation.
        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }

    void AudioManagement(bool jump,bool climb)
    {
        // If the player is currently in the run state...
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            // ... and if the footsteps are not playing...
            if (!GetComponent<AudioSource>().isPlaying)
                // ... play them.
                GetComponent<AudioSource>().Play();
        }
        else
            // Otherwise stop the footsteps.
            GetComponent<AudioSource>().Stop();

        // If the shout input has been pressed...
        if (jump)
            // ... play the shouting clip where we are.
            AudioSource.PlayClipAtPoint(jumpingClip, transform.position);





    }


//for check is it on the ground
void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnTriggerStay(Collider collider)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if (hit.transform.tag == "buliding")
            {
                if (joystick.Vertical>0.1)
                {
                    anim.SetBool("Climb", true);

                    rb.useGravity = false;
                    climb = true;
                    //transform.position += transform.forward * Time.deltaTime * sticktowallforce;
                    transform.position += transform.up * Time.deltaTime * climbspeed;
                }

            }
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "buliding")
        {
           
                anim.SetBool("Climb", false);
                rb.useGravity = true;
            climb = false;
        }

    }

}
