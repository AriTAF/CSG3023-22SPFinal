/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Krieger 
 * Last Edited: April 28, 2022
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    [Header("General Settings")]
    public int balls = 3; //number of balls/lives the player has
    public int score = 0; //player's current score
    public Text ballTxt; //reference to the balls textbox
    public Text scoreTxt; //reference to the score textbox
    public GameObject paddle; //reference to the paddle object

    [Header("Ball Settings")]
    private float initialXForce = 0f; //initial force in the X direction
    public float initialYForce; //initial force in the Y direction
    private Vector3 initialForce; //initial force of the ball
    public float speed = 10f; //speed of the ball
    public bool isInPlay; //bool to know if the ball is currently in play
    private Rigidbody rb; //reference to the rigidbody component of the ball
    [SerializeField]
    private AudioSource audioSource; //reference to the audiosource component of the ball

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        initialForce = new Vector3(initialXForce, initialYForce, 0f);
    }//end Awake()


    // Start is called before the first frame update
    void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        //Set textboxes
        ballTxt.text = "Balls : " + balls;
        scoreTxt.text = "Score : " + score;

        //Check if ball is in play
        if (!isInPlay)
        {
            if (paddle != null)
            {
                //if the paddle reference is set then set the position of the ball equal to the paddle
                SetStartingPos();
            }//end if(paddle != null)
            else
            {
                //if the paddle is null put an error in the console
                Debug.Log("error - paddle reference not set in ball.cs for " + name);
            }//end else

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isInPlay = true;
                Move();
            }//end if (Input.GetKeyDown("Space"))
        }

    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay)
        {
            //if the ball is in play, set the velocity of the ball equal to its current velocity normalized times speed
            Vector3 vel = rb.velocity;
            vel.Normalize();
            vel *= speed;
            rb.velocity = vel;
        }//end if (isInPlay)

    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddel
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()

    private void Move()
    {
        //Set the initial speed of the ball when it enters play
        rb.AddForce(initialForce);
    }//end Move()

    void OnCollisionEnter(Collision other)
    {
        
        //When this object collides with another solid object
        //play audiosource
        audioSource.Play();
        
        //get gameobject collided with
        GameObject otherGO = other.gameObject;
        if (otherGO.tag == "Brick")
        {
            //if the other object is tagged with Brick destroy it and add to the score
            score += 100;
            Destroy(otherGO);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        
        //When this object collides with a trigger

        //Get a reference to the object collided with
        GameObject otherGO = other.gameObject;

        if (otherGO.tag == "OutBounds")
        {
            balls--;
            if (balls > 0)
            {
                Invoke("SetStartingPos", 2f);
            }
        }
    }
}
