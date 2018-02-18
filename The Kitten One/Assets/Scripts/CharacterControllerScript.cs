using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControllerScript : MonoBehaviour {

    #region Variables
    private bool inGround = true;
    public bool alive = true;
    private bool running = false;

    public Transform checkGround;
    private float checkRadio = 0.07f;
    public LayerMask groundMask, deathMask;

    public float jumpForce = 3f;
    public float speed = 1.5f;

    public Animator animator;

    public int points = 0;
    public GameObject finalPoints;
    //public Text;
    #endregion

    void Awake() {
        animator = GetComponent<Animator>();

        /*Los puntos no pueden destruirse porque su variable se cargará a la pantalla de GAME OVER*/
        DontDestroyOnLoad(finalPoints);
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
            /*Si presionamos cualquiera de esas teclas y está en el suelo, saltará y la animación cambiará su variable "enSuelo"*/
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("InGround", false);
        }

        #region MOVIMIENTO: Los obstáculos se moverán como si corriera, pero con estos algoritmos simulamos aceleración y deceleración.
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(speed < 0) speed = -speed; //Si va hacia la izquierda, cambia su velocidad a la derecha, ACELERA
            if(!running)
            {
                running = true;
                animator.SetBool("Running", running);
                NotificationCenter.DefaultCenter().PostNotification(this, "Begin");
            }
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (speed > 0) speed = -speed; //Si va hacia la derecha, cambia su velocidad a la izquierda, DECELERA
            if (!running)
            {
                running = true;
                animator.SetBool("Running", running);
                //NotificationCenter.DefaultCenter().PostNotification(this, "Begin");
            }
        }
        #endregion
        //running = false;
        //animator.SetBool("Running", running);
    }

    void FixedUpdate()
    {
        if (running)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        inGround = (Physics2D.OverlapCircle(checkGround.position, checkRadio, groundMask));
        animator.SetBool("InGround", inGround);
        animator.SetBool("Running", running);
        if (alive)
        {
            points++;
            alive = !(Physics2D.OverlapCircle(checkGround.position, checkRadio, deathMask));
        }
        else
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "Dead");
            //StartCoroutine(MyCoroutine()); //Se inserta la rutina para la pantalla de MUERTE en el controlador del pj
            GetComponent<Rigidbody2D>().velocity = new Vector2(-3, GetComponent<Rigidbody2D>().velocity.y);
        }
        
        animator.SetBool("Alive", alive);
    }

    //IEnumerator MyCoroutine()
    //{
    //    yield return new WaitForSeconds(4.0f);
    //    SceneManager.LoadScene("DeathScene");
    //    //image.SetActive(true);
    //}
}
