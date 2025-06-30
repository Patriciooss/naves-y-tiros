using UnityEngine;

public class Enemigo6 : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreRafagas = 2f;
    public float velocidad = 1f;
    private float tiempoDisparo;
    public GameObject ExplosionEffect;


    void Update()
    {

        transform.Translate(Vector2.down * velocidad * Time.deltaTime);

        tiempoDisparo += Time.deltaTime;
        if (tiempoDisparo >= tiempoEntreRafagas)
        {
            DispararRafaga();
            tiempoDisparo = 0f;
        }
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void DispararRafaga()
    {
        for (int i = -1; i <= 1; i++)
        {
            GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);
            bala.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(i * 2, -5f);
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
