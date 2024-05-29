using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Netcode;

public class ThirdPersonMovement : NetworkBehaviour
{

    private CharacterController controller;
    [SerializeField] private CinemachineFreeLook myCamera;

    private Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    NetworkCommunication networkCommunication;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        networkCommunication = GetComponent<NetworkCommunication>();
    }

    public override void OnNetworkSpawn()
    {

        base.OnNetworkSpawn();

        if (IsServer)
        {
            controller.enabled = false;
            this.transform.position = new Vector3(0, 5, 2);
            controller.enabled = true;
        }
        else
        {
            controller.enabled = false;
        }

        if (IsOwner)
        {
            myCamera.Priority = 100;
            
        }
        else
        {
            enabled = false;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            networkCommunication.RotatePlayerServerRPC(Quaternion.Euler(0f, angle, 0f));

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            networkCommunication.MovePlayerServerRPC(moveDir.normalized * speed * Time.deltaTime);
            // if animator
            // networkCommunication.AnimatePlayerServerRPC(true)

            // if gravity
            //networkCommunication.MovePlayerYServerRPC(Vector3.zero);

        }
    }
}
