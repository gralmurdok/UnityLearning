using Unity.Netcode;
using UnityEngine;

public class ServerStartManager : MonoBehaviour
{
    void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
