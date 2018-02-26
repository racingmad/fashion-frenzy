using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    //Rigidbody Variable
    Rigidbody2D rb;

    //Public Variables
    public float runSpeed;
    public float jumpHeight;
    public int playerNumber;
    public GameObject hitStun;
    public Coroutine stunCheck;

    //Private Variables
    private float moveHorizontal;
    private bool IsGrounded;
    private bool IsHit;
    private bool HeadBonked;
    private bool CanJump;
    private Transform fist;

	void Start () {
        //Rigidbody variable is assigned to gameobject's rigidbody
        rb = GetComponent<Rigidbody2D>();
        
        //IsHit is set to false to keep active state
        IsHit = false;
        CanJump = true;
    }

    void FixedUpdate()
    {
        //Controller input depending on playerNumber (Can currently only be set to 1 or 2)
        moveHorizontal = Input.GetAxis("Horizontal" + playerNumber);
        float jump;
        if(CanJump)
        {
            jump = Input.GetAxis("Jump" + playerNumber);
        }
        else
        {
            jump = 0;
        }

        //Player can control character if IsHit is false
        if(!IsHit && !HeadBonked)
        {
            //Hit Stun Sprite is hidden
            hitStun.SetActive(false);

            //Can move left right and jump when on the ground using velocity
            if (IsGrounded == true)
            {
                Vector2 movement = new Vector2(moveHorizontal * runSpeed, jump * jumpHeight);
                rb.velocity = movement;
            }

            //Moves left and right in the air using velocity
            else if (IsGrounded == false)
            {
                Vector2 airRb = rb.velocity;
                airRb.x = moveHorizontal * runSpeed;
                rb.velocity = airRb;
            }
        }
        //Completely disables movement
        else if(IsHit)
        {
            //Hit stun Sprite is shown
            hitStun.SetActive(true);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
   {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb2 = collision.gameObject.GetComponent<Rigidbody2D>();
            //Makes opposing player heavier to prevent easily pushing opponent
            if (collision.gameObject.tag == "Player" && moveHorizontal != 0)
            {
                rb2.mass = 3;
            }

            Transform opponentPosition = collision.gameObject.GetComponent<Transform>();
            if (collision.gameObject.tag == "Player" && transform.position.y > opponentPosition.position.y)
            {
                Movement move = collision.gameObject.GetComponent<Movement>();
                move.CanJump = false;

                if (move.IsGrounded == false)
                {
                    move.HeadBonked = true;
                    rb2.velocity = new Vector2(0.0f, -2.0f);
    //                Debug.Log("Head Bonk Start");
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //Grounds character if touching another object
        IsGrounded = true;

        if(HeadBonked && collision.gameObject.tag != "Player")
        {
            HeadBonked = false;
 //           Debug.Log("Head Bonk End");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //Disables jumping if not collided with anything
        IsGrounded = false;

        //Returns opponent to normal mass
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb2 = collision.gameObject.GetComponent<Rigidbody2D>();
            rb2.mass = 1;
        }

        Transform opponentPosition = collision.gameObject.GetComponent<Transform>();
        if (collision.gameObject.tag == "Player" && transform.position.y > opponentPosition.position.y)
        {
            Movement move = collision.gameObject.GetComponent<Movement>();
            move.CanJump = true;
        }
    }

    //Activates hitstun state for set amount of time
    public IEnumerator GotHit()
    {
        fist = transform.Find("Fist");

        IsHit = true;
        fist.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        IsHit = false;
        fist.gameObject.SetActive(true);
    }
}
