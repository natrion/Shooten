using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private float MaxHealth;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Image healthBar;
    [SerializeField] private Joystick joystick;
    [SerializeField] GameObject GameOverScreen;

    private float health;

    private void Start()
    {
        health = MaxHealth;
    }
    public float Health
    {
        set
        {
            if (0 > value) GameOverScreen.SetActive(true);
            health = value; // Ensure health is clamped between 0 and 1
            healthBar.fillAmount = health/MaxHealth;
        }
        get { return health; }
    }
     
    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        Vector3 movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0.0f);
        transform.position -= movement * moveSpeed * Time.deltaTime;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Enemy"))
        {
            Health -= collision.gameObject.GetComponent<EnemyMonoBehaviour>().Damage * Time.deltaTime;
        }
    }
}