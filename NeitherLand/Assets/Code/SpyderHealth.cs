using UnityEngine;
using System.Collections;

public class SpyderHealth : MonoBehaviour {

    public int startingHealth = 100;	
    public int currentHealth;          
    public float sinkSpeed = 2.5f;      
    public int scoreValue = 10;        
    public AudioClip deathClip;             

    Animator anim;                   // Reference to the animator.
    AudioSource enemyAudio;             // Reference to the audio source.
    BoxCollider boxCollider;        // Reference to the capsule collider.
    Light damageLight;
    bool isDead;                 // Whether the enemy is dead.
    bool isSinking;              // Whether the enemy has started sinking through the floor.

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        damageLight = GetComponent<Light>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        enemyAudio.Play();
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        boxCollider.isTrigger = true;
        anim.SetTrigger("IsDead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }
}
