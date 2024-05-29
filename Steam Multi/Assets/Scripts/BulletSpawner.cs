using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BulletSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform InistialTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && IsOwner)
        {
            SpawnBulletServerRPC(InistialTransform.position, InistialTransform.rotation);
        }
    }

    [ServerRpc]
    private void SpawnBulletServerRPC(Vector3 position, Quaternion rotation)
    {
        GameObject InstansiatedBullet = Instantiate(bullet, position, rotation);

        InstansiatedBullet.GetComponent<NetworkObject>().Spawn();

    }
}
