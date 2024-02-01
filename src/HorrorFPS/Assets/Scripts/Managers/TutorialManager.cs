using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [System.Serializable]
    public class TutorialTask
    {
        public string description;
        public bool completed;
    }

    public List<TutorialTask> tutorialTasks = new List<TutorialTask>();
    private int currentTaskIndex = 0;
    private bool playerDamaged = false;

    public bool isActive = true;
    public TextMeshProUGUI promptText;
    public PlayerTest playerTest;
    public gun gun;
    public OpenDoor openDoor;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (isActive)    
        {
            gun.ammoCount = 0;
            gun.ammoReserve = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isActive)
        {
            openDoor.locked = true;
            UpdateTutorialText(GetCurrentTaskDescription());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TaskCompleted(0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            TaskCompleted(1);
        }

        if (isActive)
        {
            if (currentTaskIndex == 3 && !playerDamaged)
            {
                playerDamaged = true;
                playerTest.TakeDamage(80);
            }
        }
        
    }

    string GetCurrentTaskDescription()
    {
        if (currentTaskIndex < tutorialTasks.Count)
        {
            return tutorialTasks[currentTaskIndex].description;
        }
        else
        {
            openDoor.locked = false;
            openDoor.BaseInteract();
            return "";
        }
    }

    public void TaskCompleted(int taskIndex)
    {
        if (taskIndex == currentTaskIndex)
        {
            tutorialTasks[currentTaskIndex].completed = true;

            currentTaskIndex++;

            UpdateTutorialText(GetCurrentTaskDescription());
        }
    }

    void UpdateTutorialText(string newText)
    {
        promptText.text = newText;
    }
}
