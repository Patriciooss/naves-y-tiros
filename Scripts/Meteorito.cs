using UnityEngine;

public class Meteorito : MonoBehaviour
{
    public float velocidad = 3f;
    private Vector2 direccion;

    void Start()
    {
         if (transform.position.x <= 0)
        {
            direccion = new Vector2(5f, -5f).normalized;
        }
        else
        {
            direccion = new Vector2(-5f, -5f).normalized;
        }
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
        transform.Rotate(0, 0, 200 * Time.deltaTime); // Gira 200 grados por segundo
        // Destruir si sale de pantalla (fuera de cámara)
        if (transform.position.y < -6f || Mathf.Abs(transform.position.x) > 13f)
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
            
        }
    }
}
