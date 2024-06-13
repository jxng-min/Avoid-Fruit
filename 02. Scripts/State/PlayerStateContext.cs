using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _State
{
    public class PlayerStateContext : MonoBehaviour
    {
        public IPlayerState CurrentState
        {
            get; set;
        }

        private readonly PlayerCtrl m_player_ctrl;

        public PlayerStateContext(PlayerCtrl ctrl)
        {
            m_player_ctrl = ctrl;
        }

        public void Transition()
        {
            CurrentState.Handle(m_player_ctrl);
        }

        public void Transition(IPlayerState state)
        {
            CurrentState = state;
            CurrentState.Handle(m_player_ctrl);
        }
    }
}

