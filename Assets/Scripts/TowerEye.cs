using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEye : MonoBehaviour
{
    public Transform closestEnemy;
    public GameObject[] multipleEnemies;
    public Tower tower;
    public bool shouldShoot;

    // Start is called before the first frame update
    void Start()
    {
        tower = GetComponentInParent<Tower>();
    }

    // Update is called once per frame
        void Update()
    {
        closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            float distanceToClosestEnemy = Vector2.Distance(transform.position, closestEnemy.transform.position);

            if (distanceToClosestEnemy <= tower.attackRange) // Replace 'tower.attackRange' with the desired attack range of the tower
            {
                shouldShoot = true;
            }
            else
            {
                shouldShoot = false;
            }

            LookAtEnemy();

            if (shouldShoot == true)
            {
                tower.Shoot();
            }
        }
    }


    public void LookAtEnemy()
    {
        Vector2 lookDirection = closestEnemy.transform.position - transform.position;
        transform.up = new Vector2(lookDirection.x, lookDirection.y);
    }

    public Transform GetClosestEnemy()
    {
        multipleEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform enemyPos = null;

        foreach(GameObject enemies in multipleEnemies)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, enemies.transform.position);
            
            if(currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                enemyPos = enemies.transform;
            }
        }

        return enemyPos;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            shouldShoot = true;
            closestEnemy = GetClosestEnemy();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            shouldShoot = false;
        }
    }
}
