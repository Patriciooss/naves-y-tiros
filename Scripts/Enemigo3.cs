using UnityEngine;
using UnityEngine.AI;

public class Enemigo3 : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1f;
    public float velocidadMovimiento = 3f;

    private float tiempoActual;
    private Transform objetivo;
    public GameObject ExplosionEffect;

    void Start()
    {
        objetivo = GameObject.FindGameObjectWithTag("Player")?.transform;

    }

    void Update()
    {
        if (objetivo != null)
        {
            MoverHaciaJugador();
            DispararSiEsTiempo();
        }
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void MoverHaciaJugador()
    {
        Vector2 direccion = (objetivo.position - transform.position).normalized;
        transform.position += (Vector3)direccion * velocidadMovimiento * Time.deltaTime;
    }

    void DispararSiEsTiempo()
    {
        tiempoActual += Time.deltaTime;

        if (tiempoActual >= tiempoEntreDisparos)
        {
            Disparar();
            tiempoActual = 0f;
        }
    }

    void Disparar()
    {
        Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);
        SoundFXController.Instance.DisparoEnemigo(transform);
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