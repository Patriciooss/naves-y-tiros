using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    void Update()
    {
        // Si el jugador presiona Enter, reanuda el juego
        if (Input.GetKeyDown(KeyCode.Escape)) // o KeyCode.KeypadEnter si preferís el Enter del pad numérico
        {
            GameController.Instance.ResumeGame();
        }
    }
}

