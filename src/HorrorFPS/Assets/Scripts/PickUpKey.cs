using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    public Component doorCollider;
    public GameObject key;
    public PlayerTest playerTest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerStay()
    {
        if(Input.GetKey(KeyCode.E))
            {
                // doorCollider.GetComponent<BoxCollider>().enabled = true;
                playerTest.HasKey = true;
                key.SetActive(false);
            } 
    }
}
