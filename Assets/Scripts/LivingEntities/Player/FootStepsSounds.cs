using UnityEngine;
using System.Linq;

namespace Nedoshooter.Players
{using Nedoshooter.Surface;

    public class FootStepsSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SurfaceStepSound[] _stepSounds = new SurfaceStepSound[2];
        [SerializeField] private Transform _checkPoint;

        private SurfaceType _currentSurface;

        public void FixedUpdate()
        {
            if (Physics.Raycast(_checkPoint.position, Vector3.down, out RaycastHit hit) == false)
            {
                SetSurface(SurfaceType.Unknown);
                return;
            }

            if (hit.transform.gameObject.TryGetComponent<Surface>(out Surface surface) == false)
            {
                return;
            }

            if (surface.Type == _currentSurface)
            {
                return;
            }

            SetSurface(surface.Type);

        }

        public void Play()
        {
            if (_currentSurface == SurfaceType.Unknown)
            { 
                return; 
            }

            _audioSource.Play();
        }

        public void Pause()
        {
            _audioSource.Pause();
        }

        private void SetSurface(SurfaceType surfaceType)
        {
            _currentSurface = surfaceType;
            _audioSource.clip = _stepSounds.First(surface => surface.Type == surfaceType).Clip;
        }
    }
}
