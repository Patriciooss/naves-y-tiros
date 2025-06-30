using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour

{
  [SerializeField] private int speed;
  public float xMin = -2.5f, xMax = 2.5f;
  public float yMin = -4.5f, yMax = 4.5f;
  public int vidas;
  [SerializeField] public UIManager uiManager;
  Animator animator;
  public Sprite normalSprite;
  public Sprite Level2Sprite;
  public Sprite Level3Sprite;
  private SpriteRenderer spriteRenderer;
  private bool leveledUp1 = false;
  private bool leveledUp2 = false;
  private LevelUpUI levelUpUI;
  private LevelUpUIi2 levelUpUIi2;
  public GameObject EfectoLvlUp;
  private bool invulnerable = false;

  void Start()
  {
    GameManager.Instance.SetVidas(3);
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = normalSprite;
    vidas = GameManager.Instance.GetVidas();
    uiManager.ActualizarVidas(vidas);
    animator = GetComponent<Animator>();
    levelUpUI = GameObject.Find("UIController").GetComponent<LevelUpUI>();
    levelUpUIi2 = GameObject.Find("UIController2").GetComponent<LevelUpUIi2>();
    Debug.Log("Vidas iniciales del jugador: " + vidas);
    if (uiManager == null)
    {
      uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
      // uiManager = FindFirstObjectByType<UIManager>();
    }
  }

  void Update()
  {
    float moveX = Input.GetAxis("Horizontal");
    float moveY = Input.GetAxis("Vertical");

    Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0f) * speed * Time.deltaTime;

    newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
    newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);

    transform.position = newPosition;
    if (!leveledUp1 && ScoreManager.instance.score >= 1000) { LevelUp1(); levelUpUI.ShowLevelUp(); }
    if (!leveledUp2 && ScoreManager.instance.score >= 3000) { LevelUp2(); levelUpUIi2.ShowLevelUp(); }
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Enemy"))
    {
      PerderVida();
    }
  }
  public void PerderVida()
  {
    if (invulnerable) return;
    invulnerable = true;
    Invoke("QuitarInvulnerabilidad", 0.1f);
    CameraShake.instance.Shake(0.2f, 0.3f);
    DamageFlash.instance.Flash(0.3f);
    vidas--;
    GameManager.Instance.SetVidas(vidas);
    uiManager.ActualizarVidas(vidas);
    SoundFXController.Instance.SacanVidaPlayer(transform);
    if (vidas <= 0)
    {
      SoundFXController.Instance.MuerePlayer(transform);
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      gameObject.GetComponent<Collider2D>().enabled = false;
      gameObject.GetComponent<Player>().enabled = false;
      GameManager.Instance.JugadorMurio();
    }
  }
  

  void LevelUp1()
  {
    leveledUp1 = true;
    spriteRenderer.sprite = Level2Sprite;
    GameObject EfectoLvlUp1 = Instantiate(EfectoLvlUp, transform.position, Quaternion.identity);
    Destroy(EfectoLvlUp1, 2f);
    SumarVida(1);
    GameManager.Instance.SetVidas(vidas);
  }
  void LevelUp2()
  {
    leveledUp2 = true;
    spriteRenderer.sprite = Level3Sprite;
    GameObject EfectoLvlUp1 = Instantiate(EfectoLvlUp, transform.position, Quaternion.identity);
    Destroy(EfectoLvlUp1, 2f);
    SumarVida(1);
    GameManager.Instance.SetVidas(vidas);
  }
  void QuitarInvulnerabilidad()
  {
    invulnerable = false;
  }
  public void SumarVida(int cantidad)
  {
    vidas += cantidad;
    uiManager.ActualizarVidas(vidas);
    GameManager.Instance.SetVidas(vidas);
  }
  public void ReiniciarJugador()
{
    // Restaurar variables
    GameManager.Instance.ReactivarNave();
    vidas = GameManager.Instance.GetVidas();
    uiManager.ActualizarVidas(vidas);
    
    // Reiniciar posición
    transform.position = GameManager.Instance.puntoRespawn.position;

    // Reiniciar animaciones si se rompieron
    animator.Rebind(); 
    animator.Update(0f); // Esto fuerza la actualización del animator
}
}
    
 