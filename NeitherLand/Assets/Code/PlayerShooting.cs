using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.
    public Image shootImage;
    public AudioClip shootClip;

    float timer;                                    // A timer to determine when to fire.
    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    AudioSource gunAudio;                           // Reference to the audio source.

    void Awake ()
    {
        shootableMask = LayerMask.GetMask("PlayerShootable");

        gunAudio = GetComponent<AudioSource> ();
    }

    void Update ()
    {
        shootImage.enabled = false;
        timer += Time.deltaTime;

        if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot ();
        }
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.clip = shootClip;
        gunAudio.Play ();
        shootImage.enabled = true;
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        bool raycast = Physics.Raycast (shootRay, out shootHit, range, shootableMask);
        if(raycast)
        {
            SpyderHealth spyderHealth = shootHit.collider.GetComponent <SpyderHealth> ();

            if(spyderHealth != null)
            {
                spyderHealth.TakeDamage (damagePerShot/*, shootHit.point*/);
            }
        }
    }
}