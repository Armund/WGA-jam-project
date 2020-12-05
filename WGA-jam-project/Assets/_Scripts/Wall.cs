using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<BulletScript>())
            if (collider.gameObject.GetComponent<BulletScript>().notDamaged)
            {
                collider.gameObject.GetComponent<BulletScript>().Damage();
            }
    }
}
