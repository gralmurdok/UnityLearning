using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[System.Serializable]
public class VRMap
{
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map(Transform vrTarget)
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : NetworkBehaviour
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
        if (IsClient && IsOwner)
        {
            headBodyOffset = transform.position - headConstraint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsClient && IsOwner)
        {
            transform.position = headConstraint.position + headBodyOffset;
            head.Map(HeadInput.Instance.localTransform);
            leftHand.Map(LeftHandInput.Instance.localTransform);
            rightHand.Map(RightHandInput.Instance.localTransform);

            transform.rotation = Quaternion.Euler(0, headConstraint.rotation.eulerAngles.y, 0);

            //if (Quaternion.Angle(Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), Quaternion.Euler(0, headConstraint.transform.rotation.eulerAngles.y, 0)) > 90)
            //{
            //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, headConstraint.transform.rotation.eulerAngles.y, 0) * transform.rotation, 3);
            //}
        }
    }
}
