using UnityEngine;
using System.Collections;

public class SpyderAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public AudioClip chargeSound;
    public AudioClip shotSound;

    Animator anim;
    GameObject player;
    public Transform rayOrigin; // testing
    int shootableMask;
    AudioSource audioSource;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    bool isAttacking;
    bool isCharging;
    RaycastHit raycastHit;
    public Light shootLight;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
       // shootLight = GetComponentInChildren<Light>();
        shootableMask = LayerMask.GetMask("Shootable");
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject == player)
    //    {
    //        playerInRange = true;
    //    }
    //}

    void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < 70.0f)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
            Debug.DrawRay(ray.origin, ray.direction);
            bool raycast = Physics.Raycast(ray, out raycastHit, 100f, shootableMask);
            if (raycast)
            {
                if (raycastHit.transform == player.transform && !isAttacking)
                {
                    isAttacking = true;
                    anim.SetBool("AttackPlayer", true);
                }
            }
            else
            {
                isAttacking = false;
            }
        }

        if(isCharging)
        {
            shootLight.intensity += 0.1f;
        }
        else
        {
            shootLight.enabled = false;
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetBool("AttackPlayer", false);
            anim.SetTrigger("PlayerDead");
        }
    }

    public void SpyderCharge()
    {
        audioSource.clip = chargeSound;
        audioSource.Play();
        shootLight.enabled = true;
        shootLight.intensity = 0.0f;
        isCharging = true;
    }

    public void SpyderShoot()
    {
        isCharging = false;
        audioSource.clip = shotSound;
        audioSource.Play();
        shootLight.intensity = 4.0f;
        playerHealth.TakeDamage(attackDamage);
    }
}
