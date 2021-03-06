using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerActions : NetworkBehaviour
{
    [SerializeField]
    private Transform bulletOrigin;

    [SerializeField]
    private float bulletSpeed = 750f;

    void Update()
    {
        if (IsClient && IsOwner && Input.GetKeyDown(KeyCode.Space))
        {
            shootServerRpc();
        }

        if (IsClient && IsOwner && Input.GetKeyDown(KeyCode.P))
        {
            SpawnPetServerRpc(NetworkManager.LocalClientId);
        }

        if (IsClient && IsOwner && Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(GameObject.Find("Ground").gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("DAMAGE TAKEN");
        collision.gameObject.SetActive(false);
    }

    [ServerRpc]
    public void shootServerRpc()
    {
        SpawnerControl.Instance.SpawnBullet(bulletOrigin, bulletSpeed, NetworkManager.LocalClientId);
    }

    [ServerRpc]
    public void SpawnPetServerRpc(ulong id)
    {
        SpawnerControl.Instance.SpawnPlayerPet(id);
    }
}
