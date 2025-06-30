using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuInicioController : MonoBehaviour
{
    public GameObject menuUI; 
    public string nombreEscenaJuego = "Nivel1";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            SoundFXController.Instance.IniciarJuego(transform);
            StartCoroutine(IniciarDespuesDelSonido());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Nivel2");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("Nivel3");
        }
    }

    IEnumerator IniciarDespuesDelSonido()
    {
        yield return new WaitForSeconds(3f);
        
        SceneManager.LoadScene(nombreEscenaJuego);
    }
}

