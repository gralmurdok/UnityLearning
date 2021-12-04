using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandInput : Singleton<RightHandInput>
{
    public Transform localTransform
    {
        get
        {
            return transform;
        }
    }
}
