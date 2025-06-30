using UnityEngine;
using UnityEngine.UI;

public class CooldownBar2 : MonoBehaviour
{
    public Image cooldownBar; // asignar en el inspector
    public float cooldownTime = 0.5f;
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

