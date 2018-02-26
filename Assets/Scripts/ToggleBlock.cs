using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBlock : MonoBehaviour {
    public int playerNumber;
    public string blockInput;
    public int shieldDurability;

    private int positionInfluence;
    private Transform fist;
    private Rigidbody2D player;
    private int maxDurability;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector2(0.0f, 0.0f);
        fist = transform.parent.Find("Fist");
        player = transform.parent.GetComponent<Rigidbody2D>();
        positionInfluence = 1;
        maxDurability = shieldDurability;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(blockInput))
        {
            transform.localScale = new Vector2(0.8f, 1.1f);
            player.mass = 100;
            fist.gameObject.SetActive(false);
        }
        else if (Input.GetKeyUp(blockInput))
        {
            transform.localScale = new Vector2(0.0f, 0.0f);
            fist.gameObject.SetActive(true);
            player.mass = 1;
        }

        if(shieldDurability == 0)
            shieldDurability = maxDurability;
            
    }

    private void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal" + playerNumber);

        if (direction > 0)
            positionInfluence = 1;
        else if (direction < 0)
            positionInfluence = -1;

        transform.localPosition = new Vector2(0.75f * positionInfluence, 0);
    }
}
