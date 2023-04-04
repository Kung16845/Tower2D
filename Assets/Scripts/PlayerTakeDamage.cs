using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            GameManager.instance.PlayerGetDamage();
            EnemyHealth _enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            _enemyHealth.Destroy();
        }
    }
}
