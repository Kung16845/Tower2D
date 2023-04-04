using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNext : MonoBehaviour
{
    public int startIndexScene;

    public void SceneToLoad()
    {
        SceneManager.LoadScene(startIndexScene);
    }
}
