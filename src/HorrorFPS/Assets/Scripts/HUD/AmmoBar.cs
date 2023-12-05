using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour, IAmmoUpdater
{
    public Image fill;

    void Start()
    {
        fill.fillAmount = 1;
    }

    public void SetAmmo(int ammo, int maxAmmo)
    {
        fill.fillAmount = (float) ammo / maxAmmo;
    }
}
