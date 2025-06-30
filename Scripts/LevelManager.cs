using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public float duracionNivel = 60f;
    private float tiempoRestante;
    public GameObject mensajeNivelCompletado;
    public GameObject mensajeNivelCompletado2;
    public float tiempoAntesDeCargarSiguiente = 30f;
    public GameObject starEffectPrefab;
    public Transform spawnPoint;
    public FlashEffect flashEffect;
    private bool transicionIniciada = false;
    public TMP_Text puntajeTexto;
    public TMP_Text vidasGanadasTexto;
    private int vidasGanadas;
    private Player jugador;
    public GameObject mensajeJefe;
    public GameObject jefeFinalPrefab;
    public Transform spawnPointJefe;
    private GameObject jefeInstanciado;
    private bool esNivelJefe = false;
    private bool jefeDerrotado = false;

    void Start()
    {
        tiempoRestante = duracionNivel;
        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");
        if (objJugador != null)
        {
            jugador = objJugador.GetComponent<Player>();
        }
        if (SceneManager.GetActiveScene().name == "Nivel3")
        {
            esNivelJefe = true;
            Invoke("IniciarTransicionJefe", 1f);
        }
        

    }

    void Update()
    {
        if (esNivelJefe == false)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0f)
            {
                TerminarNivel();
            }
        }
    }

    void TerminarNivel()
    {
        mensajeNivelCompletado.SetActive(true);
        Invoke("ActivarTransicion", 2f);
    }

    public void ActivarTransicion()
    {
        if (transicionIniciada) return;
        transicionIniciada = true;
        mensajeNivelCompletado.SetActive(false);
        if (starEffectPrefab != null)
        {
            Instantiate(starEffectPrefab, spawnPoint.position, Quaternion.identity);
        }
        Invoke("CargarSiguienteNivel", tiempoAntesDeCargarSiguiente);
        StartCoroutine(flashEffect.PlayFlash(60f));
        MostrarRecuento();
    }
    void MostrarRecuento()
    {
        int puntaje = ScoreManager.instance.score;
        vidasGanadas = puntaje / 2000;
        puntajeTexto.text = "Puntaje final: " + puntaje.ToString();
        vidasGanadasTexto.text = "Vidas ganadas: " + vidasGanadas.ToString();
        jugador.SumarVida(vidasGanadas);
    }

    void CargarSiguienteNivel()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual + 1);
    }
    void IniciarTransicionJefe()
    {
        mensajeJefe.SetActive(true);
        Invoke("AparecerJefe", 3f);
    }
    void AparecerJefe()
    {
        mensajeJefe.SetActive(false);

        if (jefeFinalPrefab != null && spawnPointJefe != null)
        {
            jefeInstanciado = Instantiate(jefeFinalPrefab, spawnPointJefe.position, Quaternion.identity);
        }
    }
    public void JefeDerrotado()
    {
        jefeDerrotado = true;
        TerminarNivel2(); 
    }
    void TerminarNivel2()
    {
        mensajeNivelCompletado2.SetActive(true);
    }
}

