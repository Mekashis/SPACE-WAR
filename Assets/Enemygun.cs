using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    private GameObject playerShip;

    public float fireInterval = 1f;

    void Start()
    {
        playerShip = GameObject.Find("Player");
        if (playerShip == null)
        {
            Debug.LogError("Player not found!");
        }
        else
        {
            InvokeRepeating("FireBullet", 1f, fireInterval);
        }
    }

    void Update()
    {
        
    }

    void FireBullet()
    {
        if (enemyBulletPrefab != null && playerShip != null)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);

            
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
        else
        {
            Debug.LogError("Enemy bullet prefab or player ship is not assigned!");
        }
    }
}
