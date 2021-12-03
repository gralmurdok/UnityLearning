using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadControl : Singleton<HeadControl>
{
    public Transform GameObjectTransform
    {
        get { return transform.parent; }
    }
}
