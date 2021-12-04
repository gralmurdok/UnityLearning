using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandInput : Singleton<LeftHandInput>
{
    public Transform localTransform
    {
        get
        {
            return transform;
        }
    }
}
