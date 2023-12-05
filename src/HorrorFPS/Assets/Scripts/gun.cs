using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour
{
    public int MaxAmmo = 10;
    public int ammoReserve = 100;
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 3.5f;
    private int ammoCount;
    private bool isReloading = false;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animation gunMovement;
    [SerializeField]
    private AudioSource gunshot;
    [SerializeField]
    private AudioSource reload;
    [SerializeField]
    private AudioSource emptyFire;

    private float nextTimeToFire = 0f;
    private int layerMask = ~0;

    // Manage the different possible Ammo UI elements
    public HUDManager hudManager;
    public AmmoCounter ammoCounter;
    public AmmoBulletsManager ammoBulletsManager;
    public AmmoBar ammoBar;
    public gunHeat gunHeat;

    void Start()
    {
        hudManager.currentAmmoUpdater.SetAmmo(ammoCount, MaxAmmo);
        hudManager.currentAmmoUpdater.SetReserve(ammoReserve);
        // ammoCounter.SetAmmo(ammoCount, MaxAmmo);
        // gunHeat.SetAmmo(ammoCount, MaxAmmo);
        // ammoBulletsManager.createBullets(ammoCount);
    }

    void Awake()
    {
        ammoCount = MaxAmmo;
        // ammoBulletsManager.createBullets(ammoCount);
        // ammoBar.SetAmmo(ammoCount,MaxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReloading)
        {

            if ((Input.GetButtonDown("Fire1")) && (Time.time >= nextTimeToFire) && (ammoCount > 0))
            {
                nextTimeToFire = Time.time + 1f/fireRate;
                Shoot();
                ammoCount -= 1;
                hudManager.currentAmmoUpdater.SetAmmo(ammoCount, MaxAmmo);
                // ammoCounter.SetAmmo(ammoCount, MaxAmmo);
                // ammoBulletsManager.createBullets(ammoCount);
                // ammoBar.SetAmmo(ammoCount, MaxAmmo);
                // gunHeat.SetAmmo(ammoCount, MaxAmmo);
            }

            else if ((Input.GetButtonDown("Fire1")))
            {
                emptyFire.PlayOneShot(emptyFire.clip);
            }

            if (!gunMovement.isPlaying)
            {
                gunMovement.Play("idlePistol");
            }

            if (Input.GetKeyDown(KeyCode.R) && ammoCount < MaxAmmo && ammoReserve > 0)
            {
                StartCoroutine(ReloadGun());
            }
            
            
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunshot.PlayOneShot(gunshot.clip);
        gunMovement.Play("Gun Fire");

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, layerMask,QueryTriggerInteraction.Ignore))
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

    IEnumerator ReloadGun()
    {
        isReloading = true;
        // Play Animation and Sound
        gunMovement.Play("Gun Reload");
        reload.PlayOneShot(reload.clip);
        // Wait A certain Time
        yield return new WaitForSeconds(gunMovement["Gun Reload"].length);

        // Reset Ammunition Counter and reserve
        int ammoUsed = MaxAmmo - ammoCount;
        if (ammoUsed > ammoReserve) ammoUsed = ammoReserve;
        ammoCount += ammoUsed;
        ammoReserve -= ammoUsed;
        

        hudManager.currentAmmoUpdater.SetAmmo(ammoCount, MaxAmmo);
        hudManager.currentAmmoUpdater.SetReserve(ammoReserve);

        // ammoCounter.SetAmmo(ammoCount, MaxAmmo);
        // ammoBulletsManager.createBullets(ammoCount);
        // ammoBar.SetAmmo(ammoCount, MaxAmmo);
        // gunHeat.SetAmmo(ammoCount, MaxAmmo);

        isReloading = false;
    }

    public void addAmmo(int ammoAmount)
    {
        ammoReserve += ammoAmount;
        hudManager.currentAmmoUpdater.SetReserve(ammoReserve);
    }
}
