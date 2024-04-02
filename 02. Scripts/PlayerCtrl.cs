using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    private Transform m_transform;
    
    [SerializeField]
    private float m_move_speed = 80.0f;

    private float m_axis_h;

    private Animator m_animator;

    void Start()
    {
        GameManager.game_state = GameManager.GameState.PLAYING; 
        m_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        m_transform = this.gameObject.GetComponent<Transform>();
        m_animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        m_axis_h = Input.GetAxis("Horizontal");
        SetPlayerDirection();
        SetPlayerMoveAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        m_rigidbody.velocity = new Vector2(m_axis_h * m_move_speed * Time.deltaTime, m_rigidbody.velocity.y);
    }

    void SetPlayerDirection()
    {
        if(m_axis_h > 0f)
            m_transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else if(m_axis_h < 0f)
            m_transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    void SetPlayerMoveAnimation()
    {
        if(GameManager.game_state != GameManager.GameState.DEAD)
        {
            if(m_axis_h == 0)
                m_animator.SetBool("IsMove", false);
            else
                m_animator.SetBool("IsMove", true);
        }
    }

    void SetPlayerDeadAnimation()
    {
        m_animator.SetTrigger("Dead");
    }

    void SetPlayerDead()
    {
        GameManager.game_state = GameManager.GameState.DEAD;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("POOP"))
        {
            SetPlayerDeadAnimation();
            this.gameObject.GetComponent<SECallCtrl>().Click();
            Invoke("SetPlayerDead", 0.2f);
        }
    }
}
