using UnityEngine;

public class Enemigo1Movimiento : MonoBehaviour
{
    public float speed = 2f;
    public GameObject vidaPrefab;
    public GameObject ExplosionEffect;


    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }

    }
    public void DestruirEnemigo()
    {
        if (GameManager.Instance.enemigosDestruidos >= 20)
        {
            
            Instantiate(vidaPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.ReiniciarContador();
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.PerderVida();
            Destroy(gameObject);
            GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        }
        
    }
}
