using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Samples;

//[RequireComponent(typeof(NetworkObject))]
//[RequireComponent(typeof(ClientNetworkTransform))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 3.5f;

    [SerializeField]
    private float rotationSpeed = 3.5f;

    private CharacterController characterController;

    // client caches positions
    private Vector3 oldInputPosition = Vector3.zero;
    private Vector3 oldInputRotation = Vector3.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //if (IsClient && IsOwner)
        //{
            ClientInput();
        //}
    }

    private void ClientInput()
    {
        // left & right rotation
        Vector3 inputRotation = new Vector3(0, Input.GetAxis("Horizontal"), 0);

        // forward & backward direction
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        float forwardInput = Input.GetAxis("Vertical");
        Vector3 inputPosition = direction * forwardInput;

        if (oldInputPosition != inputPosition ||
            oldInputRotation != inputRotation)
        {
            characterController.SimpleMove(inputPosition * walkSpeed);
            transform.Rotate(inputRotation * rotationSpeed);
        }
    }
}