using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class OpenDoor : MonoBehaviour
{
    public PlayerTest playerTest;
    public Animation doorOpen;
    public bool locked;
    private bool isOpen = false;
    private bool isChangingState = false;


    float _closedRotation;
    float _openRotation;

    void Awake()
    {
        _closedRotation = transform.rotation.eulerAngles.y;
        _openRotation = _closedRotation - 90;

    }

    void Update()
    {
        if (playerTest.HasKey)
        {
            locked = false;
        }
    }
    
    void OnTriggerStay()
    {
        if (Input.GetKey(KeyCode.E) && !isChangingState)
        {
            isChangingState = true;
            if ((!locked) && !isOpen)
            {
                doorOpen.Play("DoorOpenIn");
                isOpen = true;
                StartCoroutine(ChangeStateAfterDelay());
                print("trying to Open");
                
            }
            else if (isOpen){
                doorOpen.Play("DoorCloseIn");
                isOpen = false;
                StartCoroutine(ChangeStateAfterDelay());
                print("trying to close");
            }
            else
            {
                isChangingState = false;
            }

        }
    }

    IEnumerator ChangeStateAfterDelay()
    {
        yield return new WaitForSeconds(doorOpen["DoorOpenIn"].length); // Change to the appropriate animation length
        isChangingState = false;
    }
}
