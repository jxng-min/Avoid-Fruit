using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _State
{
    public class PlayerMoveState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;

        public void Handle(PlayerCtrl player_ctrl)
        {
            if(!m_player_ctrl)
                m_player_ctrl = player_ctrl;

            m_player_ctrl.m_animator.SetBool("IsMove", true);
            Debug.Log("이동 상태 호출됨");
        }
        
        void FixedUpdate()
        {
            m_player_ctrl.m_rigidbody.velocity = 
                    new Vector2(m_player_ctrl.m_axis_h * 
                                m_player_ctrl.m_move_speed * 
                                Time.deltaTime, 
                                m_player_ctrl.m_rigidbody.velocity.y); 
        }
    }
}
