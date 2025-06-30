using UnityEngine;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    public GameObject levelUpText;
    public float displayTime = 2f;

    public void ShowLevelUp()
    {
        StartCoroutine(ShowLevelUpRoutine());
    }

    private System.Collections.IEnumerator ShowLevelUpRoutine()
    {
        levelUpText.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        levelUpText.SetActive(false);
    }
}
