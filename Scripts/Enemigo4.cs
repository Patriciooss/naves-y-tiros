using UnityEngine;

public class Enemigo4 : MonoBehaviour
{
    public float speed = 2f;
    public GameObject escudoVisual;
    private bool escudoActivo = true;
    public int vida = 2;

    private bool yaRecibioImpactoEsteFrame = false;
    public GameObject ExplosionEffect;
    public GameObject vidaPrefab;

    void Start()
    {
        ActivarEscudo(true);
    }

    void Update()
    {
        yaRecibioImpactoEsteFrame = false; 

        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    public void RecibirImpacto()
    {
        if (yaRecibioImpactoEsteFrame) return; 
        yaRecibioImpactoEsteFrame = true;

        if (escudoActivo)
        {
            escudoActivo = false;
            ActivarEscudo(false);
        }
        else
        {
            vida--;
            if (vida <= 0)
            {
                Morir();
            }
        }
    }

    void ActivarEscudo(bool estado)
    {
        if (escudoVisual != null)
            escudoVisual.SetActive(estado);
    }

    void Morir()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DisparoJugador"))
        {
            RecibirImpacto();
        }
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.PerderVida();
            RecibirImpacto();
            GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        }
    }
    public void DestruirEnemigo()
    {
        
        if (GameManager.Instance.enemigosDestruidos >= 35)
        {
        
            Instantiate(vidaPrefab, transform.position, Quaternion.identity);

            GameManager.Instance.ReiniciarContador();
        }
    }
}