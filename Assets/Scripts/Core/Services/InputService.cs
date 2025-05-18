using UnityEngine;

namespace Core.Services
{
    public class InputService : IInputService
    {
        public float GetAxis(string axisName) => Input.GetAxis(axisName);
        public bool GetMouseButton(int button) => Input.GetMouseButton(button);
        public Vector2 MousePosition => Input.mousePosition;
    }
}
