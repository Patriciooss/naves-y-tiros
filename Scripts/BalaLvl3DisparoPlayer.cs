using UnityEngine;


public class BalaLvl3DisparoPlayer : MonoBehaviour
{
    private Transform player;
    public float duration = 2f;
    Animator animator;
    public GameObject ExplosionEffect;
    public float growSpeed = 10f;
    BoxCollider2D boxCollider;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, duration); 
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject explosion = Instantiate(ExplosionEffect, other.transform.position, Quaternion.identity);
            Destroy(explosion, 2f);

            Destroy(other.gameObject);

            ScoreManager.instance.AddPoints(100);
            SoundFXController.Instance.MuereEnemigo(transform);

            Enemigo1Movimiento enemigo = other.GetComponent<Enemigo1Movimiento>();
            if (enemigo != null)
            {
                enemigo.DestruirEnemigo();
                GameManager.Instance.SumarEnemigoDestruido();
            }

            Enemigo4 enemigoConEscudo = other.GetComponent<Enemigo4>();
            if (enemigoConEscudo != null)
            {
                enemigoConEscudo.RecibirImpacto();
                enemigoConEscudo.DestruirEnemigo();
                GameManager.Instance.SumarEnemigoDestruido();
            }
        }
        if (other.CompareTag("Boss"))
        {
            Boss eljefe = other.GetComponent<Boss>();
            if (eljefe != null)
            {
                eljefe.TakeDamage(3);
                GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
               
            }
        }
    }
     void Update()
    {
        if (player != null)
        {
                 transform.position = player.position;
                  if (transform.localScale.y < 20f)
        {
                 transform.localScale += new Vector3(0, growSpeed * Time.deltaTime, 0);
        
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(boxCollider.size.x, transform.localScale.y);
            boxCollider.offset = new Vector2(0, transform.localScale.y / 2f); 
        }
        }
        }
    }
}
