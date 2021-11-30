using Unity.Netcode;
using UnityEngine;

public class SpawnerControl : NetworkSingleton<SpawnerControl>
{
    [SerializeField]
    private GameObject bulletPrefab;

    public void Start()
    {
        NetworkManager.Singleton.OnServerStarted += () =>
        {
            NetworkObjectPool.Instance.InitializePool();
        };
    }

    public void SpawnBullet()
    {
        if (!IsServer) return;

        GameObject go = NetworkObjectPool.Instance.GetNetworkObject(bulletPrefab).gameObject;
        go.transform.position = new Vector3(Random.Range(-4, 4), 1.0f, Random.Range(-4, 4));
        
        go.GetComponent<NetworkObject>().Spawn();
    }
}