using UnityEngine;
using System.Collections;

public class DisparoPlayer1 : MonoBehaviour
{

    public GameObject bala;   // Prefab de la bala
    public Transform firePoint;       // Punto desde donde se dispara
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;     // Tiempo entre disparos
    private float fireCooldown = 0f;
    public GameObject balaPlayerlvl2;
    
    public float bulletSpeed2 = 50f;
    public float fireRate2 = 1.2f;
    private float fireCooldown2 = 0f;
    public GameObject balaPlayerlvl3;
     
     
     private float fireCooldown3 = 0f;
     public float bulletSpeed3 = 50f;
     public float fireRate3 = 1.2f;
     
     public CooldownBar2 cooldownUI;
     public CooldownBar1 cooldownUIi;
     public CooldownBar cooldownUIii;
     

  
  void Update()
    {
        fireCooldown -= Time.deltaTime;
        fireCooldown2 -= Time.deltaTime;
        fireCooldown3 -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
            cooldownUIii.ActivarCooldown();
            SoundFXController.Instance.DisparoPlayer1(transform);
        }
         if (ScoreManager.instance.score >= 1000)
         {
         if (Input.GetKey(KeyCode.E) && fireCooldown2 <= 0f )
            {
            Shoot2();
            fireCooldown2 = fireRate2;
            cooldownUIi.ActivarCooldown();
            SoundFXController.Instance.DisparoPlayer2(transform);
            }
         }
          if (ScoreManager.instance.score >= 3000)
          {
            if (Input.GetKey(KeyCode.Q) && fireCooldown3 <= 0f )
            {
            Shoot3();
            fireCooldown3 = fireRate3;
            cooldownUI.ActivarCooldown();
            SoundFXController.Instance.DisparoPlayer3(transform);
            }
          }
    }
    void Shoot()
{
    if (firePoint == null)
    {
        Debug.LogWarning("DisparoPlayer1: firePoint no está asignado.");
        return;
    }

    GameObject bala1 = Instantiate(bala, firePoint.position, Quaternion.identity);
    Rigidbody2D rb = bala1.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.linearVelocity = Vector2.up * bulletSpeed;
    }
}
    void Shoot2()
    {   
        
        GameObject bala2 = Instantiate(balaPlayerlvl2, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bala2.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.up * bulletSpeed2;
        }
    }
    void Shoot3()
    {   
        Vector3 offset = new Vector3 (0,balaPlayerlvl3.GetComponent<SpriteRenderer>().bounds.extents.y,0);
        GameObject bala3 = Instantiate(balaPlayerlvl3, transform.position + offset, Quaternion.identity);
        Rigidbody2D rb = bala3.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.up * bulletSpeed3;
        }
    }
}



