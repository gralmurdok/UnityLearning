using Unity.Netcode;
using UnityEngine;

public class ServerStartManager : MonoBehaviour
{
    private bool serverHasStarted = false;
    private bool clientHasConnected = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !serverHasStarted) {
            NetworkManager.Singleton.StartHost();
            serverHasStarted = true;
        }

        if (Input.GetKeyDown(KeyCode.C) && !clientHasConnected)
        {
            NetworkManager.Singleton.StartClient();
            clientHasConnected = true;
        }
    }

}
