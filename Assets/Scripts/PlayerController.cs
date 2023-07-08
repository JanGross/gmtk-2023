using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 forward;
    public Camera playerCam;
    public float xLimit = 45;
    public float yLimit = 25;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = 2  * (Input.mousePosition.x / playerCam.pixelWidth) - 1;
        float mouseY = -(2 * (Input.mousePosition.y / playerCam.pixelHeight) - 1);

        Quaternion rotation = Quaternion.Euler(yLimit * mouseY, xLimit * mouseX, 0);
        playerCam.transform.rotation = rotation;
    }
}
