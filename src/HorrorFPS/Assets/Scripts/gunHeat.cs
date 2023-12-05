using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunHeat : MonoBehaviour, IAmmoUpdater
{
    public Renderer slideRenderer;
    private Material originalMaterial;
    // Start is called before the first frame update
    void Awake()
    {
        originalMaterial = slideRenderer.material;   
    }

    public void SetAmmo(int ammo, int MaxAmmo)
    {
        if (ammo == MaxAmmo)
        {
            slideRenderer.material = originalMaterial;
        }

        else
        {
            float normalizedAmmo = (float)ammo / MaxAmmo;
            Color newColor = new Color(1f, 0.5f*normalizedAmmo, 0f);

            Material updatedMaterial = new Material(originalMaterial);
            updatedMaterial.color = newColor;

            slideRenderer.material = updatedMaterial;
        }

    }
}
