using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Presentation.UI.Controls
{
    public abstract class WindowBase : MonoBehaviour
    {
        public virtual void Open()
        {
            gameObject.SetActive(true);

            transform.localScale = Vector3.zero;

            LMotion.Create(Vector3.zero, Vector3.one, 0.35f)
                .WithEase(Ease.OutBack)
                .BindToLocalScale(transform);   
        }

        public virtual void Close() => gameObject.SetActive(false);
    }
}