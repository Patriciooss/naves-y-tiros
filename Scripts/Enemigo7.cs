using UnityEngine;

public class Enemigo7 : MonoBehaviour
{

    public float tiempoEntreTeleports = 1.5f;
    public float areaX = 4.5f;  // margen para que no salga del área visible (-5 a +5)
    public float areaY = 5.5f;  // margen para que no salga del área visible (-6 a +6)
    public GameObject efectoTeleport; // opcional, un efecto visual de humo o destello

    private float temporizador;
    public Transform puntoDisparo;
    public GameObject balaPrefab;
    public float velocidad = 1f;

    void Update()
    {
        transform.Translate(Vector2.down * velocidad * Time.deltaTime);
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoEntreTeleports)
        {
            Teletransportarse();
            temporizador = 0f;
            Disparar();
        }
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

        void Teletransportarse()
    {
        if (efectoTeleport != null)
        {
            GameObject humo1 = Instantiate(efectoTeleport, transform.position, Quaternion.identity);
            Destroy(humo1, 1f);
        }
        
        float nuevaX = Random.Range(-areaX, areaX);
        float nuevaY = Random.Range(-areaY, areaY);
        transform.position = new Vector3(nuevaX, nuevaY, transform.position.z);

        if (efectoTeleport != null)
        {
            GameObject humo2 = Instantiate(efectoTeleport, transform.position, Quaternion.identity);
            Destroy(humo2, 1f);
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
    void Disparar()
    {
        Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);
        SoundFXController.Instance.DisparoEnemigo(transform);
    }
}
