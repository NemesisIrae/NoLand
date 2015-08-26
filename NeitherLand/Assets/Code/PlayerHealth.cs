using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int startingHealt = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Animator gameOverAnimator;
    public Color flashColor = new Color(1f, 0f, 0f, 0.8f);
    public Color deathColor = new Color(0f, 0f, 0f, 1.0f);

    AudioSource playerAudio;
    UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;

    bool isDead;
    bool damaged;
    void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        firstPersonController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        currentHealth = startingHealt;
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        if(isDead)
        {
            gameOverAnimator.SetBool("GameOver", true);
        }
	}

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        playerAudio.clip = deathClip;
        playerAudio.Play();

        firstPersonController.enabled = false;
    }
}
