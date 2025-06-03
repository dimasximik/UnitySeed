namespace Core.Services
{
    public interface IInputService
    {
        float GetAxis(string axisName);
        bool GetMouseButton(int button);
        UnityEngine.Vector2 MousePosition { get; }
    }
}
