using UnityEngine;
using System.Collections;

public class mainPlayers : MonoBehaviour {
    //Public Player Variables
    public Transform player1;
    public Transform player2;

    //Spawns players in game
    void Start()
    {
        Instantiate(player1);
        Instantiate(player2);
    }
}
