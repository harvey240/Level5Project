using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Interactable
{
    public PlayerTest playerTest;
    [SerializeField]
    private int healAmount = 80;

    protected override void Interact()
    {
        if (playerTest.currentHealth != playerTest.maxHealth)
        {
             TutorialManager.instance.TaskCompleted(3);

            playerTest.Heal(healAmount);
            Debug.Log("Interacted with " + gameObject.name);
            gameObject.SetActive(false);
        }

        else
        {
            StartCoroutine(HealthFullDebugMessage());
        }

    }

    IEnumerator HealthFullDebugMessage()
    {
        String defaultPromptMessage = promptMessage;
        promptMessage = "Health Already Full";
        yield return new WaitForSeconds(0.8f);
        promptMessage = defaultPromptMessage;
    }
}
