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
    public float attackRange;
    public int level;
    public int maxLevel;
    public int upgradeCost;
    public Animator anim;
    public GameObject lvupEffect;
    public TextMeshProUGUI UpgradeCosetText;
    public float sellPercentage = 0.5f;

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
    public int sellValue = 20; // ค่าขายของหอเรือรบ
    public void SellTower()
    {
        // เรียกใช้เมธอด ReleaseBuildPlace ก่อนทำลายหอป้อม
        TowerManager towerManager = FindObjectOfType<TowerManager>();
        if (towerManager != null)
        {
            towerManager.ReleaseBuildPlace(transform.position);
        }

        // เพิ่มเงินให้กับผู้เล่นตามค่าขายของหอเรือรบ
        GameManager.instance.AddGold(sellValue);
        // ทำลายออบเจกต์ของหอเรือรบ
        Destroy(gameObject);
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
