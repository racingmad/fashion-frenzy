using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaunchGame : MonoBehaviour {
    //Button variable
    Button btn;

    //Variable stores button component and adds listener to start game when clicked
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickBtnPlay);
    }

    //Starts game on button click
    private void OnClickBtnPlay()
    {
        SceneManager.LoadScene("Gameplay Test");
    }
}
