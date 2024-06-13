using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _State
{
    public class PlayerDeadState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;
        public void Handle(PlayerCtrl player_ctrl)
        {
            if(!m_player_ctrl)
                m_player_ctrl = player_ctrl;

            m_player_ctrl.m_animator.SetTrigger("Dead");
            Debug.Log("게임 오버 상태 호출됨");

            Invoke("SetPlayerDead", 0.2f);
        }

        void SetPlayerDead()
        {
            GameManager.game_state = GameManager.GameState.DEAD;
        }
    }
}
