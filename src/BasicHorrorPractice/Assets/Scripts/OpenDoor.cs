using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class OpenDoor : MonoBehaviour
{
    public PickUpKey pickUpKey;
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
    
    void OnTriggerStay()
    {
        if (Input.GetKey(KeyCode.E) && !isChangingState)
        {
            isChangingState = true;
            if ((!locked || pickUpKey.HasKey) && !isOpen)
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
        }
    }



    IEnumerator ChangeStateAfterDelay()
    {
        yield return new WaitForSeconds(doorOpen["DoorOpenIn"].length); // Change to the appropriate animation length
        isChangingState = false;
    }
}
