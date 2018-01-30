using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {


    private bool inGround = true;
    public bool alive = true;
    private bool running = false;

    public Transform checkGround, checkFront;
    private float checkRadio = 0.07f;
    public LayerMask groundMask, deathMask;

    public float jumpForce = 3f;
    public float speed = 1.5f;

    public Animator animator;

    public int points = 0;
    //public Text;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //running = false;
        if((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            && inGround)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            //animator.SetFloat("SpeedY", speed.y);
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(speed < 0) speed = -speed; //If going right -> then go left
            if(!running)
            {
                running = true;
                animator.SetBool("Running", running);
                NotificationCenter.DefaultCenter().PostNotification(this, "Begin");
            }
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (speed > 0) speed = -speed; //If going left -> then go right
            if (!running)
            {
                running = true;
                animator.SetBool("Running", running);
                //NotificationCenter.DefaultCenter().PostNotification(this, "Begin"); <= Don't start when you go left!
            }
        }
        //running = false;
        //animator.SetBool("Running", running);
    }

    void FixedUpdate()
    {
        if (running)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        inGround = (Physics2D.OverlapCircle(checkGround.position, checkRadio, groundMask) || Physics2D.OverlapCircle(checkFront.position, checkRadio, groundMask));
        animator.SetBool("InGround", inGround);
        animator.SetBool("Running", running);
        if(alive)
        {
            points++;
            alive = !(Physics2D.OverlapCircle(checkGround.position, checkRadio, deathMask));
        }
        animator.SetBool("Alive", alive);
    }
}
