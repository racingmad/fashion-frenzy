using UnityEngine;
using System.Collections;

public class CloseSpike : MonoBehaviour {
    //Public Variables
    public float timerBeforeClosing;
    public float closeRate;

    //Private Variables
    private SpriteRenderer stageSprite;
    private bool IsClosing = false;

    //IEnumerator Variable
    IEnumerator startCheck;

    //Assigns SpriteRenderer to current gameobject's SpriteRenderer
    void Awake()
    {
        stageSprite = GetComponent<SpriteRenderer>();
    }

    //Activates main Coroutine for script
    private void Start()
    {
        startCheck = StartClosing();
        StartCoroutine(startCheck);
    }

    //Gradually closes spikes when IsClosing is true
    void FixedUpdate () {
        if(IsClosing == true)
        {
            //Detects whether spike is flipped to determine direction
            if (stageSprite.flipX == true)
            {
                transform.Translate(closeRate, 0, Time.deltaTime);
            }
            else
            {
                transform.Translate(-closeRate, 0, Time.deltaTime);
            }
        }

    }

    //Stops Closing Spikes
    void CloseStop()
    {
        if(startCheck == null)
        {
            IsClosing = false;
        }
    }

    //Stops script behavior when activated
    public void stopAllSpikes()
    {
        if (startCheck == null && IsClosing == true)
        {
            CloseStop();
        }
        else if (startCheck != null && IsClosing == false)
        {
            StopCoroutine(startCheck);
        }
    }

    //Timer before spike closing process
    IEnumerator StartClosing()
    {
        yield return new WaitForSeconds(timerBeforeClosing);
        IsClosing = true;
        startCheck = null;
    }
}
