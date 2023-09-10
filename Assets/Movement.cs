using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Movement : MonoBehaviour
{
    [SerializeField] private float StartWorth;
    [SerializeField] private float BuletSpeed;
    [SerializeField] private float BuletTime;
    [SerializeField] private float PlayerDamage;
    [SerializeField] private float MaxHealth;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Image healthBar;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Joystick joystick2;
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject bulet;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] public static TextMeshProUGUI CoinText;
    [SerializeField] private TextMeshProUGUI StartWorthText;

    private float health;
    public static float Money=1;
    private bool BuyThing()
    {
        if (Money > StartWorth * (float)SpawnEnemy.WaveNumber*(float)SpawnEnemy.Difficulty)
        {
            Money -= StartWorth * (float)SpawnEnemy.WaveNumber * (float)SpawnEnemy.Difficulty;
            CoinText.text = Money.ToString() + "c";

            return true;
        }
        else return false;
    }
    public void UpdateCost() 
    {
        StartWorthText.text = "Cost:" + (StartWorth * (float)SpawnEnemy.WaveNumber * (float)SpawnEnemy.Difficulty).ToString() + "c";
    }
    public void ChangeStats(float maxHealth)
    {
        if (BuyThing() == true) moveSpeed += maxHealth;
    }
    public void ChangeMaxHealth(float maxHealth)
    {
        if (BuyThing() == true) { MaxHealth += maxHealth; Health = health;  }

    }
    public void ChangeHealth()
    {
        if (BuyThing() == true)  Health = MaxHealth;

    }
    public void ChangebuletSpeed(float buletSpeed)
    {
        if (BuyThing() == true) moveSpeed += buletSpeed;

    }
    public void ChangeBuletDamage(float BuletDamage)
    {
        if (BuyThing() == true) PlayerDamage += BuletDamage;

    }
    public void ChangeReload(float Reload)
    {
        if (BuyThing() == true) BuletTime = 1/(1/ BuletTime + Reload) ;

    }
    public void ChangeSpeed(float speed)
    {
        if (BuyThing() == true) moveSpeed += speed;

    }
    private AudioSource walkingSound;
    private AudioSource ShootingSound;
    private AudioSource HitSound;
    private void Start()
    {
        walkingSound = gameObject.GetComponents<AudioSource>()[0];
        ShootingSound = gameObject.GetComponents<AudioSource>()[1];
        HitSound = gameObject.GetComponents<AudioSource>()[3];
        CoinText = coinText;
        health = MaxHealth;
        StartCoroutine(ShootOnTime());
    }
    public float Health
    {
        set
        {
            if (0 > value) { GameOverScreen.SetActive(true); transform.GetChild(0).parent = transform.parent; Destroy(gameObject); gameObject.GetComponents<AudioSource>()[2].Play(); }
            health = value; // Ensure health is clamped between 0 and 1
            healthBar.fillAmount = health/MaxHealth;
        }
        get { return health; }
    }
     
    private void FixedUpdate()
    {
        transform.GetChild(1).rotation = Quaternion.FromToRotation(transform.up, new Vector3(joystick2.Horizontal, joystick2.Vertical, transform.position.z).normalized) * transform.rotation;

        MoveCharacter();
    }
    private IEnumerator ShootOnTime()
    {
        bool DoTHis = true;
        while (DoTHis == true)
        {
            Shoot();
            yield return new WaitForSeconds(BuletTime);
        }
    }
    void Shoot()
    {
        if (joystick2.Horizontal != 0 & joystick2.Vertical != 0 )
        {
            GameObject buletCopy =  Instantiate(bulet);
            buletCopy.transform.position = transform.position;
            buletCopy.transform.GetComponent<MoveBulet>().movingPosition = new Vector3(joystick2.Horizontal, joystick2.Vertical, 0)  * BuletSpeed;
            buletCopy.transform.GetComponent<MoveBulet>().damage = PlayerDamage;

            ShootingSound.volume = Random.Range(0.5f, 0.8f);
            ShootingSound.pitch = Random.Range(0.5f, 2f);
            ShootingSound.Play();

        }
    }

    private void MoveCharacter()
    {
        Vector3 movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0.0f);
        transform.position += movement * moveSpeed * Time.deltaTime;
        
        if (movement == Vector3.zero & walkingSound.isPlaying == true)  gameObject.GetComponent<AudioSource>().Stop(); 
        if (movement != Vector3.zero & walkingSound.isPlaying == false) gameObject.GetComponent<AudioSource>().Play();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Enemy"))
        {
            if (HitSound.isPlaying == false) HitSound.Play();
            Health -= collision.gameObject.GetComponent<EnemyMonoBehaviour>().Damage * Time.deltaTime;
        }
    }


}