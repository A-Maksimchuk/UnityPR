using System;
using UnityEngine;

namespace Services
{
    public interface IInputService
    {
        Action<Vector2> OnFire { get; set; }
        Action OnSkillChange { get; set; }
        Vector2 GetAxis();
        Vector2 MousePosition();
    }
}