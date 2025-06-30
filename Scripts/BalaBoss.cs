using UnityEngine;

public class BalaBoss : MonoBehaviour
{
    public float velocidad = 8f;

    void Update()
    {
        transform.Translate(Vector2.down * velocidad * Time.deltaTime);
        if (transform.position.y <= -6f ||  transform.position.y >= 6f || transform.position.x >= 6f || transform.position.x <= -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.PerderVida();
            }
            Destroy(gameObject); // Destruir la bala tras el impacto
        }
        else if (!other.CompareTag("Enemy")) // Para no destruirse si toca otro enemigo
        {
            Destroy(gameObject); // Destruir si choca con otra cosa
        }

    }
}

