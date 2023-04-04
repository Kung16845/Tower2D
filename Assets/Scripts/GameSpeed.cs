using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSpeed : MonoBehaviour
{
    public int gameSpeed = 1;
    public TextMeshProUGUI gameSpeedText;

    // Update is called once per frame
    void Update()
    {
        //Set the ui Text
        gameSpeedText.text = "X" +gameSpeed.ToString();
        //update speed of our game depend on our gamespeed
        Time.timeScale = gameSpeed;
    }

    public void ChangeSpeed()
    {
        //as long as gameSpeed is not 4 allow us to increase level
        if(gameSpeed < 4 )
        {
            gameSpeed++;
            AudioManager.instance.PlaySFX(3);
        }
        // if game speed reach to 4 with the next push button condition is false and give gameSpeed value of 1
        else if(gameSpeed == 4 )
        {
            gameSpeed = 1;
            AudioManager.instance.PlaySFX(3);
        }
    }

}
