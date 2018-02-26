using UnityEngine;
using System.Collections;

public class Punch : MonoBehaviour {
    //Public Variables
    public float knockback;
    public string punchInput;
    public int playerNumber;

    //Private Variables
    private bool facingRight = true;
    private float punchDistance = 2;
    private Coroutine punchCheck;
    private bool moveBlocked;


    //Character starts in the right facing position
    private void Start()
    {
        facingRight = true;
        punchDistance = 2;
        moveBlocked = false;
    }

    //Checks to initiate punch animation through coroutine variable
    void Update () {
        if(punchCheck == null)
        {
            if (Input.GetKeyDown(punchInput))
            {
                punchCheck = StartCoroutine(QuickPunch());
            }
        }
    }

    //Changes punch direction depending on horizontal input
    private void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal" + playerNumber);

        if (direction > 0)
        {
            facingRight = true;
            punchDistance = 2;
        }
        else if (direction < 0)
        {
            facingRight = false;
            punchDistance = -2;
        }
    }

    //Punch animation
    IEnumerator QuickPunch()
    {
        transform.localPosition = new Vector2 (punchDistance, 0.0f);
        yield return new WaitForSeconds(0.15f);
        transform.localPosition = new Vector2(0.0f, 0.0f);
        yield return new WaitForSeconds(0.050f);

        punchCheck = null;
    }


    private void OnTriggerEnter2D(Collider2D opponent)
    {
        if(opponent.gameObject.tag == "Block")
        {
            moveBlocked = true;
            GameObject block = opponent.gameObject;
            ToggleBlock shield = opponent.GetComponent<ToggleBlock>();
            shield.shieldDurability--;

            if(shield.shieldDurability <= 0)
            {
                StartCoroutine(KillShield(block));
            }
        }
        //Applies knockback to opponent
        if (opponent.gameObject.tag == "Player")
        {
            Rigidbody2D opposingHitbox = opponent.gameObject.GetComponent<Rigidbody2D>();
            Vector2 hitReaction;
            int direction;

            //Determines direction of knockback
            if(facingRight == true)
                direction = 1;
            else
                direction = -1;

            hitReaction = new Vector2(120.0f * direction, 80.0f);
            opposingHitbox.AddForce(hitReaction * knockback);

            Movement move = opponent.gameObject.GetComponent<Movement>();

            if(move.stunCheck != null)
            {
                StopCoroutine(move.stunCheck);
            }

//            Transform shield = opponent.gameObject.transform.GetChild(2);
//            ToggleBlock shieldCheck = shield.GetComponent<ToggleBlock>();
//           Debug.Log(shieldCheck.shieldUp);

            //Activates hitstun coroutine from Movement script
            if (moveBlocked == false)
                move.stunCheck = StartCoroutine(opponent.GetComponent<Movement>().GotHit());

            if(moveBlocked == true)
                moveBlocked = false;
        }
    }

    IEnumerator KillShield(GameObject shield)
    {
        shield.transform.localScale = new Vector2(0.0f, 0.0f);

        shield.SetActive(false);
        Rigidbody2D opposingHitbox = shield.transform.parent.gameObject.GetComponent<Rigidbody2D>();
        opposingHitbox.mass = 1;

        yield return new WaitForSeconds(4.0f);

        shield.SetActive(true);
    }
}
