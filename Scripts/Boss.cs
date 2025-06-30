using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject bulletPrefab;
    public Transform[] firePoints;

    private int phase = 1;
    private float shootTimer;
    public Sprite normalSprite;
    public Sprite Level2Sprite;
    public Sprite Level3Sprite;
    public float moveSpeed = 2f;
    public float moveRange = 5f; // Qué tan lejos puede moverse desde su punto inicial
    private Vector3 startPosition;
    private int moveDirection = 8; // 1 = derecha, -1 = izquierda
    public GameObject ExplosionEffect;
    public GameObject ExplosionEffect2;
    Animator animator;
    public float radioMovimiento = 3f;
    public float velocidadAngular = 1.5f;
    private float angulo;
    public float velocidadSeguimiento = 4f;
    [SerializeField] private Vector3 centroMovimiento;
    private Color colorOriginal;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;  

    void Start()
    {
        currentHealth = maxHealth;
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        if (spriteRenderer == null)
        {
            spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite  = normalSprite;
        colorOriginal = spriteRenderer.color;
    }

    void Update()
    {
        HandlePhases();
        HandleShooting();
        if (phase == 1 || phase == 2 || phase == 3)
        {
            spriteTransform.Rotate(0f, 0f, 100f * Time.deltaTime);
        }
    }

   void HandlePhases()
    {
        if (currentHealth <= maxHealth * 0.33f && phase < 3)
        {
            phase = 3;
            spriteRenderer.sprite = Level3Sprite;
        }
        else if (currentHealth <= maxHealth * 0.66f && phase < 2)
        {
            phase = 2;
            spriteRenderer.sprite = Level2Sprite;
        }
        if (phase == 1)
        {
            transform.Translate(Vector3.right * moveDirection * moveSpeed * Time.deltaTime);

            float offsetX = transform.position.x - startPosition.x;

            if (offsetX > moveRange)
            {
                moveDirection = -1;
                transform.position = new Vector3(startPosition.x + moveRange, transform.position.y, transform.position.z);
            }
            else if (offsetX < -moveRange)
            {
                moveDirection = 1;
                transform.position = new Vector3(startPosition.x - moveRange, transform.position.y, transform.position.z);
            }
        }
        else if (phase == 2)
        {
            angulo += velocidadAngular * Time.deltaTime;
            float x = centroMovimiento.x + Mathf.Cos(angulo) * radioMovimiento;
            float y = centroMovimiento.y + Mathf.Sin(angulo) * radioMovimiento;
            transform.position = new Vector3(x, y, transform.position.z);
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * angulo + 300f);
        }
        else if (phase == 3)
        {
            angulo += velocidadAngular * Time.deltaTime;
            float x = centroMovimiento.x + Mathf.Sin(angulo) * radioMovimiento;
            float y = centroMovimiento.y + Mathf.Sin(angulo * 2f) * radioMovimiento * 0.5f;
            transform.position = new Vector3(x, y, transform.position.z);
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * angulo + 100f);
        }
    }

    void HandleShooting()
    {
        shootTimer += Time.deltaTime;

        float shootInterval = (phase == 1) ? 1f : (phase == 2) ? 0.7f : 0.5f;

        if (shootTimer >= shootInterval)
        {
            foreach (Transform point in firePoints)
            {
                Instantiate(bulletPrefab, point.position, point.rotation);
            }
            shootTimer = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashOnHit());
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    IEnumerator FlashOnHit()
{
    spriteRenderer.color = Color.red;
    yield return new WaitForSeconds(0.1f);
    spriteRenderer.color = colorOriginal;
}
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.PerderVida();
            GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            TakeDamage(1);
        }
        
    }

    public float GetHealthPercent() 
    {
     
        return currentHealth / maxHealth;
    }
    IEnumerator ExplosionesEnCadena()
{
    float delayEntreExplosiones = 0.15f;
    for (int i = 0; i < 10; i++)
    {
        Vector3 posicionAleatoria = transform.position + (Vector3)Random.insideUnitCircle * 1.5f;
        Instantiate(ExplosionEffect2, posicionAleatoria, Quaternion.identity);
        yield return new WaitForSeconds(delayEntreExplosiones);
    }
}

  [System.Obsolete]
  void Die()
    {
    StartCoroutine(ExplosionesEnCadena());
    LevelManager manager = FindObjectOfType<LevelManager>();
    if (manager != null)
    {
        manager.JefeDerrotado();
    }

    Destroy(gameObject);
}
}
