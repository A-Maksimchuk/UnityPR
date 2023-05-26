using System;
using UnityEngine;

namespace Services
{
    public class InputService:MonoBehaviour,IInputService
    {
        public Action<Vector2> OnFire { get; set; }
        public Action OnSkillChange { get; set; }

        public Vector2 GetAxis()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        public Vector2 MousePosition()
        {
            return Input.mousePosition;
        }

        private void Update()
        {
            if(Input.GetMouseButton(0))
                OnFire?.Invoke(Input.mousePosition);
        }
    }
}