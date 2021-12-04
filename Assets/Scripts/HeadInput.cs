using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadInput : Singleton<HeadInput>
{
    public Transform localTransform
    {
        get
        {
            return transform;
        }
    }
}
