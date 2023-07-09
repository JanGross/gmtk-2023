using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{
    private Vector3 initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(initialRotation.x, transform.rotation.eulerAngles.y, initialRotation.z));
    }
}
