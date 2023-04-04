using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    public Button[] lvlButtons;

    private void Start()
    {
        int level = PlayerPrefs.GetInt("levelAt", 2);

        for(int i = 0;i < lvlButtons.Length; i++)
        {
            if(i + 2 > level)
            {
                lvlButtons[i].interactable = false;
            }
        }
    }
}
