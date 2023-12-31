using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBulletsManager : MonoBehaviour, IAmmoUpdater
{
    public GameObject bulletPrefab;
    
    public void SetAmmo(int ammoCount, int MaxAmmo)
    {
        createBullets(ammoCount);
    }

    public void createBullets(int currentAmmo)
    {
        clearAllBullets();

        for (int i=0; i<currentAmmo; i++)
        {
            createBullet();
        }
    }

    public void createBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.SetParent(transform);
    }

    public void clearAllBullets()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

}
