using UnityEngine;

public class BalaLvl2DisparoPlayer : MonoBehaviour
{
    Animator animator;
    public GameObject ExplosionEffect;
  void Start()
  {
    animator = GetComponent<Animator>();
    animator.SetTrigger("Vamos");
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Enemy"))
    {
      GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
      Destroy(explosion, 2f); // Destruye la explosión 2 segundos después

      ScoreManager.instance.AddPoints(100); // SUMA 100 PUNTOS por enemigo

      SoundFXController.Instance.MuereEnemigo(transform);

      Destroy(other.gameObject);

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
                eljefe.TakeDamage(2);
                Destroy(gameObject);
            }
        }
  }
   void Update()
    {
        
        if (transform.position.y >6f && gameObject != null)
        {
            Destroy(gameObject);
        }
 
   }
}
