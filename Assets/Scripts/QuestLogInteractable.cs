using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogInteractable : MonoBehaviour
{

    public GameObject journal;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("MOUSE DOWN ON INVENTORY");
        journal.SetActive(true);
        playerController.cameraMovement = false;
    }
    
    public void CloseJournal()
    {
        Cursor.lockState = CursorLockMode.Locked;
        journal.SetActive(false);
        playerController.cameraMovement = true;
    }
}
