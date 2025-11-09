using System;
using UnityEngine;

namespace CashCourses
{
    public class PlayAnimationEvent : MonoBehaviour
    {
        private Player _player;

        private void Start()
        {
            _player = GetComponentInParent<Player>();
        }

        public void OnAnimationOver()
        {
            _player.SetAttackingOver();
        }
    }
}
