using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int target = 0;
    public Transform exitPoint;
    public Transform[] wayPoint;
    public float navTimeUpdate;
    public float currentNavTime;
    private Transform enemy;
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.isDead != true)
        {
            if(wayPoint != null)
            {
                currentNavTime += Time.deltaTime;

                if(currentNavTime > navTimeUpdate)
                {
                    if(target == 12)
                    {
                        transform.Rotate(new Vector3(0, 180, 0));
                    }
                    if(target < wayPoint.Length)
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, wayPoint[target].position, currentNavTime);
                        
                    }
                    else
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, currentNavTime);
                    }

                    currentNavTime = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "wp")
        {
            target += 1;
        }
    }
}
