using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField]
    private Transform bulletOrigin;

    [SerializeField]
    private float bulletSpeed = 750f;

    private NetworkVariable<Vector3> networkPlayerPosition = new NetworkVariable<Vector3>();

    private void Start()
    {
        if (IsClient && IsOwner)
        {
            Vector3 initialPosition = new Vector3(Random.Range(-4, 4), 0f, Random.Range(-4, 4));
            RandomizePlayerStartPositionServerRpc(initialPosition);
        }
    }

    void Update()
    {
        if (IsClient && IsOwner && Input.GetKeyDown(KeyCode.Space))
        {
            shootServerRpc();
        }

        if(transform.position != networkPlayerPosition.Value)
        {
            transform.position = networkPlayerPosition.Value;
        }
    }

    [ServerRpc]
    public void RandomizePlayerStartPositionServerRpc(Vector3 initialPosition)
    {
        networkPlayerPosition.Value = initialPosition;
    }

    [ServerRpc]
    public void shootServerRpc()
    {
        SpawnerControl.Instance.SpawnBullet(bulletOrigin, bulletSpeed);
    }
}
