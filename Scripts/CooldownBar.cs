using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    public Image cooldownBar; // asignar en el inspector
    public float cooldownTime = 1f;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownBar.fillAmount = 1 - (cooldownTimer / cooldownTime);
        }
    }

    public void ActivarCooldown()
    {
        cooldownTimer = cooldownTime;
        cooldownBar.fillAmount = 0;
    }
}
