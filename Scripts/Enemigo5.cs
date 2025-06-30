using UnityEngine;

public class Enemigo5 : MonoBehaviour
{
    public float velocidad = 4f;
    private Transform jugador;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (jugador != null)
        {
            Vector2 direccion = (jugador.position - transform.position).normalized;
            transform.Translate(direccion * velocidad * Time.deltaTime);
        }
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.PerderVida();
            Destroy(gameObject);
        }
    }
}
