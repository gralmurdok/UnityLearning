using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map(Transform vrTarget)
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingPositionOffset);
    }
}

public class VRRig : MonoBehaviour
{
    [SerializeField]
    float turnSmoothness;

    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    [SerializeField]
    private Transform headConstraint;

    [SerializeField]
    private Vector3 headBodyOffset;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward,
            Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map(HeadInput.Instance.localTransform);
        leftHand.Map(LeftHandInput.Instance.localTransform);
        rightHand.Map(RightHandInput.Instance.localTransform);
    }
}
