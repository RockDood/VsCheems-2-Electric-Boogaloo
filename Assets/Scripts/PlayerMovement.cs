using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{

    public CharacterController controller;

    public Transform gndChk;
    public float gndDist = 0.4f;
    public LayerMask gndMask;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHei = 3f;

    public float mSens = 100f;

    public Transform pBody;
    public Transform Camera;
    public GameObject PlayerCamera;
    bool mouseLocked = true;



    float xRot = 0f;

    Vector3 velocity;
    bool isGnd;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (this.isLocalPlayer)
            PlayerCamera.SetActive(true);
        else
            PlayerCamera.SetActive(false);
    }

    void Update()
    {

        if (!this.isLocalPlayer)
        {
            if (this.GetComponent<AudioListener>() != null)
                this.GetComponent<AudioListener>().enabled = !this.GetComponent<AudioListener>().enabled;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mouseLocked = !mouseLocked;
            if (mouseLocked)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }

        isGnd = Physics.CheckSphere(gndChk.position, gndDist, gndMask);

        if (isGnd && velocity.y < 0)
        {
            controller.slopeLimit = 45.0f;
            velocity.y = -2f;

        }

        if (mouseLocked)
        {
            float mX = Input.GetAxis("Mouse X") * mSens * Time.deltaTime;
            float mY = Input.GetAxis("Mouse Y") * mSens * Time.deltaTime;

            xRot -= mY;
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            Camera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            pBody.Rotate(Vector3.up * mX);

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = this.transform.right * x + this.transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGnd)
            {
                controller.slopeLimit = 100.0f;
                velocity.y = Mathf.Sqrt(jumpHei * -2 * gravity);
            }
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
