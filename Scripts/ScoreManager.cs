using UnityEngine;
using TMPro; 
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public TMP_Text scoreText;
    public GameObject habilidadIconoLvl2;
    public GameObject habilidadIconoLvl1;

    void Awake()
    {
        instance = this;
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreDisplay();
        RevisarDesbloqueo();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "" + score.ToString();
    }

    void RevisarDesbloqueo()
    {
        if (score >= 1000 && !habilidadIconoLvl1.activeSelf)
        {
            habilidadIconoLvl1.SetActive(true);
        }
        if (score >= 3000 && !habilidadIconoLvl2.activeSelf)
        {
            habilidadIconoLvl2.SetActive(true);
        }
        
    }
}

