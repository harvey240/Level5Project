using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    HUDType hudType = new HUDType();
    
    public IAmmoUpdater currentAmmoUpdater;
    public IHealthUpdater currentHealthUpdater;

    public IAmmoUpdater ammoCounter, ammoBulletsManager, ammoBar, gunHeat;
    public IHealthUpdater healthNumber, heartHealthManager, healthBar, healthVignette;

    public GameObject numericalHUD;
    public GameObject discreteHUD;
    public GameObject barHUD;
    public GameObject hueHUD;
    public gunHeat gunHeatScript;
    void Awake()
    {
        ammoCounter = numericalHUD.GetComponentInChildren<IAmmoUpdater>();
        ammoBulletsManager = discreteHUD.GetComponentInChildren<IAmmoUpdater>();
        ammoBar = barHUD.GetComponentInChildren<IAmmoUpdater>();
        gunHeat = gunHeatScript;
        
        healthNumber = numericalHUD.GetComponentInChildren<IHealthUpdater>();
        heartHealthManager = discreteHUD.GetComponentInChildren<IHealthUpdater>();
        healthBar = barHUD.GetComponentInChildren<IHealthUpdater>();
        healthVignette = hueHUD.GetComponentInChildren<IHealthUpdater>();

        if (hudType == HUDType.Numerical)
        {
            currentAmmoUpdater = ammoCounter;
            currentHealthUpdater = healthNumber;
            SetHUDVersion(1);
        }
        else if (hudType == HUDType.Discrete)
        {
            currentAmmoUpdater = ammoBulletsManager;
            currentHealthUpdater = heartHealthManager;
            SetHUDVersion(2);
        }
        else if (hudType == HUDType.Bar)
        {
            currentAmmoUpdater = ammoBar;
            currentHealthUpdater = healthBar;
            SetHUDVersion(3);
        }
        else if (hudType == HUDType.Hue)
        {
            currentAmmoUpdater = gunHeat;
            currentHealthUpdater = healthVignette;
            SetHUDVersion(4);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetHUDVersion(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetHUDVersion(2);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetHUDVersion(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetHUDVersion(4);
        }
    }

    public void SetHUDVersion(int version)
    {
        numericalHUD.SetActive(false);
        discreteHUD.SetActive(false);
        barHUD.SetActive(false);
        hueHUD.SetActive(false);

        switch(version)
        {
            case 1:
                numericalHUD.SetActive(true);
                currentAmmoUpdater = ammoCounter;
                currentHealthUpdater = healthNumber;
                break;
            case 2:
                discreteHUD.SetActive(true);
                currentAmmoUpdater = ammoBulletsManager;
                currentHealthUpdater = heartHealthManager;
                break;
            case 3:
                barHUD.SetActive(true);
                currentAmmoUpdater = ammoBar;
                currentHealthUpdater = healthBar;
                break;
            case 4:
                hueHUD.SetActive(true);
                currentAmmoUpdater = gunHeat;
                currentHealthUpdater = healthVignette;
                break;
            case 0:
                break;

        }
    }

    enum HUDType
    {
        Numerical,
        Discrete,
        Bar,
        Hue,
        None
    };
}
