using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSpawnCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject m_poop;

    void Start()
    {  
        MakePoop();
    }

    void MakePoop()
    {
        GameObject poop = Instantiate(m_poop, this.gameObject.GetComponent<Transform>().position + new Vector3(Random.Range(-2.5f, 2.6f), 0, 0), Quaternion.identity);

        float rand_scale_seed = Random.Range(3.0f, 5.1f);
        poop.GetComponent<Transform>().localScale = new Vector2(rand_scale_seed, rand_scale_seed);

        float rand_gravity_seed = Random.Range(1f, 10f);
        poop.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        poop.GetComponent<Rigidbody2D>().velocity = Vector2.down * rand_gravity_seed; 

        Invoke("MakePoop", Random.Range(0f, 0.6f));
    }
}
