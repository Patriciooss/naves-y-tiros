using UnityEngine;
using UnityEngine.UI;

public class BossVida : MonoBehaviour
{
    public Image barraRelleno;
    private Boss boss;

    void Update()
    {
        if (boss == null)
        {
            GameObject bossGO = GameObject.FindWithTag("Boss");
            if (bossGO != null)
            {
                boss = bossGO.GetComponent<Boss>();
            }
        }

        if (boss != null)
        {
            float porcentaje = boss.GetHealthPercent();
            barraRelleno.fillAmount = porcentaje;
        }
    }
}
