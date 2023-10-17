using UnityEngine;

public class gun : MonoBehaviour
{
    public int MaxAmmo = 10;
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 3.5f;
    private int ammoCount;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animation gunMovement;
    private AudioSource gunshot;

    private float nextTimeToFire = 0f;

    public AmmoCounter ammoCounter;

    void Start()
    {
        // ammoCount = MaxAmmo;
        // gunshot = GetComponent<AudioSource>();
        ammoCounter.SetAmmo(ammoCount, MaxAmmo);
    }

    void Awake()
    {
        gunshot = GetComponent<AudioSource>();
        ammoCount = MaxAmmo;
        // ammoCounter.SetAmmo(ammoCount, MaxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Fire1")) && (Time.time >= nextTimeToFire) && (ammoCount > 0))
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
            ammoCount -= 1;
            ammoCounter.SetAmmo(ammoCount, MaxAmmo);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Play Animation and Sound
            // Wait A certain Time
            // Reset Ammunition Counter
            ammoCount = MaxAmmo;
            ammoCounter.SetAmmo(ammoCount, MaxAmmo);
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunshot.PlayOneShot(gunshot.clip);
        gunMovement.Play("Gun Fire");

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
                Debug.Log("HIT!");
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
