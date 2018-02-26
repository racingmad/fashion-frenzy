using UnityEngine;
using System.Collections;

public class PlayerSpiked : MonoBehaviour {
    //Variable for other spike gameobject
    public GameObject companionSpike;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroys colliding player and stops spikes to prepare end of game
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            GetComponent<CloseSpike>().stopAllSpikes();
            companionSpike.GetComponent<CloseSpike>().stopAllSpikes();
        }
    }
}
