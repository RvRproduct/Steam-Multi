using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkCommunication : NetworkBehaviour
{
    CharacterController characterController;
    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    [ServerRpc]
    public void MovePlayerServerRPC(Vector3 position)
    {
        characterController.Move(position);
    }

    [ServerRpc]
    public void RotatePlayerServerRPC(Quaternion rotation)
    {
        this.transform.rotation = rotation;
    }

    [ServerRpc]
    public void MovePlayerYServerRPC(Vector3 position)
    {
        characterController.Move(position);
    }

    [ServerRpc]
    public void AnimatePlayerServerRPC(bool state)
    {
        animator.SetBool("IsRunning", state);
    }


}
