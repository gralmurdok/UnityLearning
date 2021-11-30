using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField]
    private float bulletLifeTime = 1.5f;

    void Start()
    {
        Invoke("DestroyItself", bulletLifeTime);
    }

    void DestroyItself()
    {
        gameObject.SetActive(false);
    }
}
