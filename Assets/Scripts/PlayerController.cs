using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public Vector3 forward;
    public Camera playerCam;
    public float xLimit = 45;
    public float yLimit = 25;
    public bool cameraMovement = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There can only be one instance of the CharacterManager class");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (cameraMovement)
            {
                float mouseX = 2 * (Input.mousePosition.x / playerCam.pixelWidth) - 1;
                float mouseY = -(2 * (Input.mousePosition.y / playerCam.pixelHeight) - 1);

                Quaternion rotation = Quaternion.Euler(yLimit * mouseY, xLimit * mouseX, 0);
                playerCam.transform.rotation = rotation;
            }
        }
    }
}
