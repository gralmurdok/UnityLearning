using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class VRAnimatorController : NetworkBehaviour
{
    public float speedThreshold = 0.1f;
    [Range(0, 1)]
    public float smoothing = 1;
    private Animator animator;
    private Vector3 previousPos;

    // NETWORK
    private NetworkVariable<Vector3> animationStateNetwork = new NetworkVariable<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        previousPos = HeadInput.Instance.localTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsClient && IsOwner)
        {
            clientAnimationInput();
        }

        updateAnimation();
    }

    private void clientAnimationInput()
    {
        // compute the speed
        Vector3 headsetSpeed = (HeadInput.Instance.localTransform.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;
        // local speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = HeadInput.Instance.localTransform.position;

        // set animator values
        float previousDirectionX = animator.GetFloat("DirectionX");
        float previousDirectionY = animator.GetFloat("DirectionY");

        float isMoving = headsetLocalSpeed.magnitude > speedThreshold ? 1 : 0;
        float directionX = Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing);
        float directionY = Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing);

        syncronizeAnimationServerRpc(new Vector3(isMoving, directionX, directionY));

        // In case of flies
        //animator.SetBool("IsMoving", headsetLocalSpeed.magnitude > speedThreshold);
        //animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
        //animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
    }

    private void updateAnimation()
    {
        if (animationStateNetwork.Value.x > 0)
        {
            animator.SetBool("IsMoving", true);
        } else
        {
            animator.SetBool("IsMoving", false);
        }

        animator.SetFloat("DirectionX", animationStateNetwork.Value.y);
        animator.SetFloat("DirectionY", animationStateNetwork.Value.z);
    }

    [ServerRpc]
    private void syncronizeAnimationServerRpc(Vector3 animationState)
    {
        animationStateNetwork.Value = animationState;
    }
}
