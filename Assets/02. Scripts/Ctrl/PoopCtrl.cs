using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Ground"))
            Destroy(this.gameObject);
    }
}
