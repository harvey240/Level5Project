using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    public GameObject ammoCount;

    private TextMeshProUGUI ammoCountText;

    void Awake()
    {
        ammoCountText = ammoCount.GetComponent<TextMeshProUGUI>();
    }

    public void SetAmmo(int ammo, int maxAmmo)
    {
        ammoCountText.text = ammo.ToString() + "/" + maxAmmo.ToString();
    }

}
