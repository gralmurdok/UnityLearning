using Unity.Netcode;
using UnityEngine;

public class SpawnerControl : NetworkSingleton<SpawnerControl>
{
    [SerializeField]
    private GameObject bulletPrefab;

    public void Start()
    {
        NetworkObjectPool.Instance.InitializePool();
    }

    public void SpawnBullet(Transform bulletOrigin, float bulletSpeed, ulong id)
    {
        if (!IsServer) return;

        Vector3 origin = bulletOrigin.transform.position;
        GameObject go = NetworkObjectPool.Instance.GetNetworkObject(bulletPrefab).gameObject;
        go.transform.position = origin;
        go.GetComponent<Rigidbody>().AddForce(bulletOrigin.transform.forward * bulletSpeed, ForceMode.VelocityChange);
        go.GetComponent<NetworkObject>().SpawnWithOwnership(id);
    }
}