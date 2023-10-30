using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Interactable
{
    public PlayerTest playerTest;
    [SerializeField]
    private int healAmount = 30;

    protected override void Interact()
    {
        playerTest.Heal(healAmount);
        Debug.Log("Interacted with " + gameObject.name);
        gameObject.SetActive(false);
    }
}
