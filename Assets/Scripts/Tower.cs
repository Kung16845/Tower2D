using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{
    public Projectile bullet;
    public Transform[] firePoints;
    public float shotPerSecond;
    private float nextShotTime;

    public int level;
    public int maxLevel;
    public int upgradeCost;
    public Animator anim;
    public GameObject lvupEffect;
    public TextMeshProUGUI UpgradeCosetText;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        UpgradeCosetText.text = upgradeCost.ToString(); 
    }

    public void AddLevel()
    {
        if(upgradeCost <= GameManager.instance.currentGold && level < maxLevel)
        {
            level++;
            GameManager.instance.ReduceGold(upgradeCost);
            anim.SetTrigger("UG");
            shotPerSecond++;
            Instantiate(lvupEffect, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(10);
        }
    }

    public void Shoot()
    {
        if(nextShotTime <= Time.time)
        {
            foreach(Transform firepoint in firePoints)
            {
                Projectile _bullet = Instantiate(bullet, firepoint.position, firepoint.rotation);
            }

            nextShotTime = Time.time + (1 / shotPerSecond);
        }
    }
}
