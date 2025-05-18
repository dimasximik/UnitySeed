using System.Collections;
using Gameplay.Player;
using LitMotion;
using UnityEngine;
using Zenject;

namespace Presentation.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] Transform      gunMuzzle;
        [SerializeField] ParticleSystem shootFx;

        Rigidbody2D     _rb;
        SpriteRenderer  _sr;
        PlayerPresenter _presenter;

        Color     _defaultColor;   
        Coroutine _flashRoutine;   

        public Rigidbody2D Body      => _rb;
        public Transform   GunMuzzle => gunMuzzle;

        [Inject] void Construct(PlayerPresenter presenter) => _presenter = presenter;

        void Awake()
        {
            _rb           = GetComponent<Rigidbody2D>();
            _sr           = GetComponent<SpriteRenderer>();
            _defaultColor = _sr.color;                       
        }

        void Update() => _presenter.Tick();
        void FixedUpdate() => _presenter.FixedTick();

        public void PlayShootFx() => shootFx.Play();

        public void PlayDamageFx()
        {
            if (_flashRoutine != null)          
                StopCoroutine(_flashRoutine);

            _flashRoutine = StartCoroutine(FlashRed());

            var start = transform.localScale;
            LMotion.Punch.Create(Vector3.zero, Vector3.one * .15f, .25f)
                         .WithFrequency(8)
                         .WithDampingRatio(.35f)
                         .Bind(d =>
                         {
                             if (this != null)
                                 transform.localScale = start + d;
                         });
        }

        IEnumerator FlashRed()
        {
            _sr.color = Color.red;
            yield return new WaitForSeconds(.12f);

            if (this != null)
                _sr.color = _defaultColor;      

            _flashRoutine = null;               
        }
    }

    public interface IPlayerView
    {
        Rigidbody2D Body      { get; }
        Transform   GunMuzzle { get; }

        void PlayShootFx();
        void PlayDamageFx();
    }
}
