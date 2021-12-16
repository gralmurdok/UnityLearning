using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class WeaponControl : NetworkBehaviour
{
    [SerializeField]
    Transform weaponBarrel;

    [SerializeField]
    float bulletSpeed;
    public void shoot()
    {
        if (IsClient && IsOwner)
        {
            shootBulletServerRpc();
        }
    }

    [ServerRpc]
    private void shootBulletServerRpc()
    {
        SpawnerControl.Instance.SpawnBullet(weaponBarrel, bulletSpeed, NetworkManager.LocalClientId);
    }
}
