using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed;
    public GameObject Explosion;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();

        CheckDestroyOutOfScreen();
    }

    void MoveEnemy()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;
    }

    void CheckDestroyOutOfScreen()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("PlayerBullet") || (col.CompareTag("ast")))
        {
            PExplosion();
            Destroy(gameObject);
            
        }
    }
    void OnDestroy()
    {
        
        gameController.EnemyDestroyed();
    }
    void PExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        explosion.transform.position = transform.position;
    }

}
