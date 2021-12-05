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
    private VRRig vrRig;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<VRRig>();
        previousPos = HeadInput.Instance.localTransform.position;
    }

    // Update is called once per frame
    void Update()
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


        animator.SetBool("IsMoving", headsetLocalSpeed.magnitude > speedThreshold);
        animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
        animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
    }
}
