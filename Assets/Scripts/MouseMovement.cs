using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    float xRotate = 0f;
    float yRotate = 0f;

    public float  topClamp = -90f;
    public float bottomClamp = 90f;
    // Start is called before the first frame update
    void Start()
    {
        //Locking Cursor in the middle
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse Updates
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Implement Rotation
        xRotate -= mouseY;

        // Clamp Rotate
        xRotate = Mathf.Clamp(xRotate, topClamp, bottomClamp);

        // Rotate Around Y axis
        yRotate += mouseX;

        transform.localRotation = Quaternion.Euler(xRotate, yRotate, 0f);

    }
}
