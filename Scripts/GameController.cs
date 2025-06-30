using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; } = null;
    private bool isPaused = false;
    private AudioSource SoundBGController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); // Asegurate de destruir el GameObject, no solo el script
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

     private void Update()
    {
        // Buscamos el AudioSource si aún no lo encontramos
        if (SoundBGController== null)
        {
            GameObject musicObj = GameObject.Find("SoundBGController");
            if (musicObj != null)
            {
                SoundBGController = musicObj.GetComponent<AudioSource>();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
                SoundFXController.Instance.Pausa(transform);
            }
            else
            {
                ResumeGame();
                
            }
        }
    }

    private void PauseGame()
    {
        if (!SceneManager.GetSceneByName("Pausa").isLoaded)
        {
            SceneManager.LoadScene("Pausa", LoadSceneMode.Additive);
            Time.timeScale = 0f;
            isPaused = true;

            if (SoundBGController != null && SoundBGController.isPlaying)
            {
                SoundBGController.Pause();
            }
        }
    }

    public void ResumeGame()
    {
        if (SceneManager.GetSceneByName("Pausa").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Pausa");
            Time.timeScale = 1f;
            isPaused = false;

            if (SoundBGController != null && !SoundBGController.isPlaying)
            {
                SoundBGController.Play();
            }
        }
    }
}

