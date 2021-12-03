using Unity.Netcode;
using UnityEngine;

public class ServerStartManager : MonoBehaviour
{
    private bool serverHasStarted = false;
    private bool clientHasConnected = false;

    private void Start()
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

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S) && !serverHasStarted) {
    //        NetworkManager.Singleton.StartHost();
    //        serverHasStarted = true;
    //    }

    //    if (Input.GetKeyDown(KeyCode.C) && !clientHasConnected)
    //    {
    //        NetworkManager.Singleton.StartClient();
    //        clientHasConnected = true;
    //    }
    //}

}
