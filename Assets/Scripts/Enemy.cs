using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Add rotationSpeed variable
    public float rotationSpeed;

    // Existing code
    public int target = 0;
    public Transform exitPoint;
    public Transform[] wayPoint;
    public float navTimeUpdate;
    public float currentNavTime;
    private Transform enemy;
    private EnemyHealth enemyHealth;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemy = GetComponent<Transform>();
    }
    private bool hasRotated = false;

    void Update()
    {
        if (enemyHealth.isDead != true && !PauseGame.gameIsPaused)
        {
            if (wayPoint != null)
            {
                currentNavTime += Time.deltaTime;

                if (currentNavTime > navTimeUpdate)
                {
                    if (target < wayPoint.Length)
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, wayPoint[target].position, currentNavTime);
                    }
                    else
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, currentNavTime);
                    }

                    currentNavTime = 0;
                }

                // Check if the enemy is at waypoint 20 and has not rotated yet
            if (target == 20 && !hasRotated)
            {
                // Rotate the enemy
                transform.Rotate(new Vector3(0, 180, 0));

                // Set the hasRotated flag to true
                hasRotated = true;
            }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "wp")
        {
            target += 1;
        }
    }
}
