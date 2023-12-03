using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Players : MonoBehaviour
{
    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;
    [SerializeField] private GameObject pos3;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private float speed;
    [SerializeField] private float boostedSpeed = 2.0f;

    public float fireInterval = 0.2f;
    public int bulletsPerSalvo = 5;
    public float cooldownTime = 3f;
    public float reloadTime = 5f;
    public float bulletFillDelay = 2f;

    private bool isCooldown = false;
    private int bulletsFired = 0;

    public Text bulletInfoText;
    private float bulletReloadTimer = 0f;
    private bool isReloading = false;

    void Start()
    {
        UpdateBulletInfoText();
    }

    void Update()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? boostedSpeed : speed;

        if (Input.GetKeyDown(KeyCode.Space) && !isCooldown && !isReloading)
        {
            FireBullets();
        }

        if (bulletsFired == bulletsPerSalvo && !isCooldown && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction, currentSpeed);

        if (isReloading)
        {
            bulletReloadTimer -= Time.deltaTime;

            if (bulletReloadTimer <= 0f)
            {
                StartCoroutine(FillBullets());
            }
        }
    }

    void Move(Vector2 direction, float currentSpeed)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * currentSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void FireBullets()
    {
        GetComponent<AudioSource>()?.Play();

        GameObject bullet1 = Instantiate(pos1);
        bullet1.transform.position = pos2.transform.position;

        GameObject bullet2 = Instantiate(pos1);
        bullet2.transform.position = pos3.transform.position;

        bulletsFired++;
        UpdateBulletInfoText();

        if (bulletsFired == bulletsPerSalvo)
        {
            isCooldown = true;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
        bulletsFired = 0;
        UpdateBulletInfoText();
    }

    IEnumerator Reload()
    {
        bulletReloadTimer = reloadTime;
        UpdateBulletInfoText();

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        bulletReloadTimer = 0f;
        StartCoroutine(FillBullets());
    }

    IEnumerator FillBullets()
    {
        while (bulletsFired < bulletsPerSalvo)
        {
            bulletsFired++;
            UpdateBulletInfoText();
            yield return new WaitForSeconds(bulletFillDelay);
        }

        if (bulletsFired == bulletsPerSalvo)
        {
            bulletsFired = 0;
            isReloading = false;
            UpdateBulletInfoText();
            yield break;
        }

        while (bulletsFired > 0)
        {
            bulletsFired--;
            UpdateBulletInfoText();
            yield return new WaitForSeconds(2f);
        }

        isReloading = false;
        UpdateBulletInfoText();
    }

    void UpdateBulletInfoText()
    {
        if (isReloading)
        {
            bulletInfoText.text = "Reloading: " + Mathf.CeilToInt(bulletReloadTimer).ToString();
        }
        else
        {
            string textToShow = bulletsFired == bulletsPerSalvo ? "Reloading:" : "Bullets:";
            bulletInfoText.text = textToShow + " " + (bulletsPerSalvo - bulletsFired);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.CompareTag("Enemy") || col.CompareTag("EnemyBullet") || col.CompareTag("ast")))
        {
            PExplosion();
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void PExplosion()
    {
        GameObject explosionObject = Instantiate(Explosion);
        explosionObject.transform.position = transform.position;
    }
}
