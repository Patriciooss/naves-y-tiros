using UnityEngine;

public class Bala : MonoBehaviour

{
    public GameObject ExplosionEffect;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            ScoreManager.instance.AddPoints(100);

            Enemigo4 enemigoConEscudo = other.GetComponent<Enemigo4>();
            if (enemigoConEscudo != null)
            {
                enemigoConEscudo.RecibirImpacto();
                enemigoConEscudo.DestruirEnemigo();
                GameManager.Instance.SumarEnemigoDestruido();
            }
            else
            {

                Enemigo1Movimiento enemigo = other.GetComponent<Enemigo1Movimiento>();
                if (enemigo != null)
                {
                    enemigo.DestruirEnemigo();
                }

                Destroy(other.gameObject);
                GameManager.Instance.SumarEnemigoDestruido();
            }

            Destroy(gameObject);
            SoundFXController.Instance.MuereEnemigo(transform);
        }
        if (other.CompareTag("Boss"))
        {
            Boss eljefe = other.GetComponent<Boss>();
            if (eljefe != null)
            {
                eljefe.TakeDamage(1);
                Destroy(gameObject);
            }
        }
}


    void Update()
    {

        if (transform.position.y > 6f && gameObject != null)
        {
            Destroy(gameObject);
        }

    }
}

