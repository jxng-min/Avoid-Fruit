using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoopSpawnCtrl : MonoBehaviour
{
    public GameObject[] m_fruits;
    private GameObject m_cur_fruit;

    void Start()
    {  
        MakeFruit();
    }

    void MakeFruit()
    {
        if(GameManager.Instance.State == GameManager.GameState.PLAYING)
        {
            m_cur_fruit = m_fruits[Random.Range(0, m_fruits.Length)];
            GameObject fruit = Instantiate(m_cur_fruit, this.gameObject.GetComponent<Transform>().position + new Vector3(Random.Range(-2f, 2.1f), 0, 0), Quaternion.identity);

            float rand_scale_seed = Random.Range(0.3f, 0.8f);
            fruit.GetComponent<Transform>().localScale = new Vector2(rand_scale_seed, rand_scale_seed);

            float rand_gravity_seed = Random.Range(1f, 10f);
            fruit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            fruit.GetComponent<Rigidbody2D>().velocity = Vector2.down * rand_gravity_seed; 
        }
        Invoke("MakeFruit", Random.Range(0f, 0.6f));
    }
}
