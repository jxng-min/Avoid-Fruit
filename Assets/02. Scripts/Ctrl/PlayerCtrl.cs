using System.Collections;
using System.Collections.Generic;
using _EventBus;
using _State;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Rigidbody2D m_rigidbody;
    public Transform m_transform;
    public float m_move_speed = 80.0f;
    public Animator m_animator;
    public JoyStickValue m_value;

    private IPlayerState m_stop_state, m_move_state, m_dead_state;
    private PlayerStateContext m_player_state_context;

    private Quaternion PlayerDirection
    {
        get { return m_transform.rotation; }
        set { m_transform.rotation = value; }
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.SETTING, GameManager.Instance.Setting);
        GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        GameEventBus.Subscribe(GameEventType.PAUSE, GameManager.Instance.Pause);
        GameEventBus.Subscribe(GameEventType.DEAD, GameManager.Instance.Dead);

        GameEventBus.Publish(GameEventType.PLAYING);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.SETTING, GameManager.Instance.Setting);
        GameEventBus.Unsubscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        GameEventBus.Unsubscribe(GameEventType.PAUSE, GameManager.Instance.Pause);
        GameEventBus.Unsubscribe(GameEventType.DEAD, GameManager.Instance.Dead);
    }

    private void Start()
    {
        GameEventBus.Publish(GameEventType.PLAYING);

        m_player_state_context = new PlayerStateContext(this);

        m_stop_state = gameObject.AddComponent<PlayerStopState>();
        m_move_state = gameObject.AddComponent<PlayerMoveState>();
        m_dead_state = gameObject.AddComponent<PlayerDeadState>();

        m_player_state_context.Transition(m_stop_state);
 
        m_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        m_transform = this.gameObject.GetComponent<Transform>();
        m_animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(m_value.m_joy_touch.x > 0f)
            PlayerDirection = Quaternion.Euler(0f, 0f, 0f);
        else if(m_value.m_joy_touch.x < 0f)
            PlayerDirection = Quaternion.Euler(0f, 180f, 0f);

        SetPlayerMoveAnimation();
    }

    private void FixedUpdate()
    {
        float joy_value = 0f;
        if(m_value.m_joy_touch.x < 0.0f)
            joy_value = -1f;
        else if(m_value.m_joy_touch.x > 0.0f)
            joy_value = 1f;
            
        m_rigidbody.linearVelocity = 
                new Vector2(joy_value * m_move_speed * Time.deltaTime, m_rigidbody.linearVelocity.y); 
    }

    public void StopPlayer()
    {
        m_player_state_context.Transition(m_stop_state);
    }

    public void MovePlayer()
    {
        m_player_state_context.Transition(m_move_state);
    }

    public void DeadPlayer()
    {
        m_player_state_context.Transition(m_dead_state);
    }

    private void SetPlayerMoveAnimation()
    {
        if(GameManager.Instance.State == GameManager.GameState.PLAYING)
            if(m_value.m_joy_touch == Vector2.zero)
                StopPlayer();
            else
                MovePlayer();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("POOP"))
        {
            GameEventBus.Publish(GameEventType.DEAD);
            SoundManager.Instance.PlayerDie();
        }
    }
}
