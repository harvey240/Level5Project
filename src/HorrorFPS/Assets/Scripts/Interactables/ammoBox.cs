using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoBox : Interactable
{
    public gun gun;
    [SerializeField]
    private int ammoAmount = 20;

    protected override void Interact()
    {
        TutorialManager.instance.TaskCompleted(0);
        gun.addAmmo(ammoAmount);
        gameObject.SetActive(false);
    }
}
