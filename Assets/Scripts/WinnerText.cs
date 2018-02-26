using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerText : MonoBehaviour {
    //Private Variables
    private Text winner;
    private int winnerIdentity;

    public int countdownTimer;
    private string timerText;
    private bool timerRunning = false;

    //Takes text component from gameobject
    private void Start()
    {
        winner = GetComponent<Text>();
        StartCoroutine(Countdown());
    }

    //Displays winning text for currently active player
    public void ShowText(GameObject opponent)
    {
        if(timerRunning == true)
        {
            StopAllCoroutines();
            timerRunning = false;
            winner.text = "";
        }

        if(winner.text == "")
        {
            winnerIdentity = opponent.GetComponent<Movement>().playerNumber;
            winner.text = "Player " + winnerIdentity + " Wins!";
            StartCoroutine(FinishDelay());
        }
    }

    //Timer before main menu
    IEnumerator FinishDelay()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("StartMenu");
    }

    IEnumerator Countdown()
    {
        timerRunning = true;
        while(countdownTimer > 0)
        {
            timerText = countdownTimer.ToString();
            winner.text = timerText;
            yield return new WaitForSeconds(1.0f);
            countdownTimer--;
        }
        winner.text = "";
        timerRunning = false;
        yield return null;
    }
}
