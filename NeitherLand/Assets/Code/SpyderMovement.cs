using UnityEngine;
using System.Collections;

public class SpyderMovement : MonoBehaviour {

    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    SpyderHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    Animator anim;


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<SpyderHealth>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        // If the enemy and the player have health left...
      //  if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        if(!anim.GetBool("AttackPlayer") && playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
        {
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);
            nav.enabled = true;
        }
        else
        {
            nav.enabled = false;
        }
    }
 
    public void NavAgentEnabled(bool enabled)
    {
        nav.enabled = enabled;
    }
}
