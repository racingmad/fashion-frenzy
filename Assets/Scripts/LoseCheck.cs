using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCheck : MonoBehaviour {
    //GameObject Variables
    GameObject text;
    public GameObject opponent;

    //Finds winner text gameobject and assigns it to variable
    private void Start()
    {
        text = GameObject.Find("Canvas/Winner");
    }

    //Shows opponent as winner through text when gameobject is destroyed
    private void OnDestroy()
    {
        //Communicates with function from WinnerText script
        text.GetComponent<WinnerText>().ShowText(opponent);
    }
}
