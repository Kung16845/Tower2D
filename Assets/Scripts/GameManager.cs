using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int nextSceneToLoad;
    public float waitingTime;
    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int enemiesOnScreen;
    public int enemiesPerWave;
    public int currentGold;
    public TextMeshProUGUI goldtext;
    public TextMeshProUGUI CurentWaveText;
    public TextMeshProUGUI CurentHealthText;
    public int maxWave;
    public int currentWave;
    public int maxHealth = 3;
    public int currentHealth;
    public GameObject winWindow;
    public GameObject loseWindow;
    public bool gameIsPaused;

    private int enemiesSpawnedInWave;
    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        
    }

    // Update is called once per frame
   void Update()
    {
        gameIsPaused = PauseGame.gameIsPaused;
        goldtext.text = currentGold.ToString();
        CurentWaveText.text = currentWave.ToString() + "/" + maxWave.ToString();
        CurentHealthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();

        if (!gameIsPaused)
        {
            if (currentWave < maxWave && enemiesOnScreen == 0)
            {
                currentWave++;
                maxEnemiesOnScreen++;
                //waitingTime -= 0.1f;
                enemiesSpawnedInWave = 0;
                StartCoroutine(Spawn());
            }
            else if (currentWave == maxWave && enemiesOnScreen == 0)
            {
                winWindow.SetActive(true);
                StopAllCoroutines();
            }
        }
    }


    public void AddGold(int amount)
    {
        currentGold += amount;
    }

    public void ReduceGold(int amount)
    {
        currentGold -= amount;
    }

    public void PlayerGetDamage()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            loseWindow.SetActive(true);
            currentHealth = 0;
        }
    }
    IEnumerator Spawn()
    {
        while (enemiesSpawnedInWave < enemiesPerWave)
        {
            if (!gameIsPaused)
            {
                GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)] as GameObject);
                newEnemy.transform.position = spawnPoint.transform.position;
                enemiesOnScreen++;
                enemiesSpawnedInWave++;
                yield return new WaitForSecondsRealtime(waitingTime);
            }
        }

        yield return new WaitForSecondsRealtime(waitingTime);
        if (currentWave < maxWave)
        {
            StartCoroutine(Spawn());
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneToLoad);

        if (nextSceneToLoad > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneToLoad);
        }
    }
    
    public void ReplyLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() { SceneManager.LoadScene(0); }

    
}
