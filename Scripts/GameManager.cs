using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int enemigosDestruidos = 0;
    public int vidasJugador = 3;
    public GameObject jugadorPrefab;
    public Transform puntoRespawn;
    public UIManager uiManager;
    private bool estaGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Suscribirse al evento de escena cargada
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Buen hábito: desuscribirse
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cada vez que cargas una escena, intenta encontrar referencias de nuevo
        if (jugadorPrefab == null)
        {
            var jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                jugadorPrefab = jugador;
            }
        }

        if (puntoRespawn == null)
        {
            var obj = GameObject.Find("PuntoRespawn");
            if (obj != null)
            {
                puntoRespawn = obj.transform;
            }
        }

        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }
    }

    private void Update()
    {
        if (estaGameOver && Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarJuego();
            HacerRespawn();
        }
    }


    public void SumarEnemigoDestruido()
    {
        enemigosDestruidos++;
    }

    public void ReiniciarContador()
    {
        enemigosDestruidos = 0;
    }

    public void SetVidas(int vidas)
    {
        vidasJugador = vidas;
        Debug.Log("GameManager: Vidas actualizadas a " + vidasJugador);
    }

    public int GetVidas()
    {
        return vidasJugador;
    }


    public void JugadorMurio()
    {
        if (uiManager != null)
        {
            uiManager.ActualizarVidas(vidasJugador);
        }


        if (vidasJugador <= 0)
        {
            
            estaGameOver = true;
            uiManager.MostrarGameOver();  // con null check por si sigue sin encontrarse
        }
    }
    void HacerRespawn()
    {
        if (puntoRespawn == null)
        {
            GameObject obj = GameObject.Find("PuntoRespawn");
            if (obj != null)
            {
                puntoRespawn = obj.transform;
            }
            else
            {
                Debug.LogWarning("No se encontró el PuntoRespawn en la escena.");
                return;
            }
        }
            jugadorPrefab.transform.position = puntoRespawn.position;
            jugadorPrefab.GetComponent<Player>().enabled = true;
            jugadorPrefab.GetComponent<Player>().ReiniciarJugador();
    }
    void ReiniciarJuego()
    {
        ReiniciarContador();
        SetVidas(3);
        uiManager.ActualizarVidas(3);
        uiManager.OcultarGameOver();

        estaGameOver = false;

        // Hacemos respawn
        
    }
    public void ReactivarNave()
    {
        jugadorPrefab.GetComponent<SpriteRenderer>().enabled = true;
        jugadorPrefab.GetComponent<Collider2D>().enabled = true;

    }
}
