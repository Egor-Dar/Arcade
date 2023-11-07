using System;
using Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace CharacterSystem.Player
{
    public class JoystickListener : MonoBehaviour
    {
        [SerializeField] private DynamicJoystick dynamicJoystick;
        public Action<Vector2> UpdateDirectionEvent;
        private bool _isStop;
        public void UpdateStop(bool value) => _isStop = value;

        private void FixedUpdate()
        {
            if (_isStop) return;
            UpdateDirectionEvent?.Invoke(dynamicJoystick.Direction);
        }
    }
}