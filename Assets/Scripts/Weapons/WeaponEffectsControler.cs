using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [RequireComponent(typeof(IFirearm))]
    public class WeaponEffectsControler : MonoBehaviour
    {
        [Header("Audio Effects")]
        [SerializeField] private AudioClip _boltClick;
        [SerializeField] private AudioClip _shot;

        [Header("Visual Effects")]
        [SerializeField] private Transform _barrelEndPoint;
        [SerializeField] private ParticleSystem _muzzleFlash;

        private IFirearm _gun;
        private AudioSource _gunAudioSource;


        private void Awake()
        {
            _gun = GetComponent<IFirearm>();
            _gunAudioSource = transform.parent.parent.GetComponent<AudioSource>();
        }

        private void CockBolt()
        {
            _gunAudioSource.PlayOneShot(_boltClick);
        }

        private void Shoot()
        {
            _gunAudioSource.PlayOneShot(_shot);
            Instantiate(_muzzleFlash, _barrelEndPoint);
        }


        private void OnEnable()
        {
            _gun.Attacked += Shoot;
            _gun.BoltCocked += CockBolt;
        }

        private void OnDisable()
        {
            _gun.Attacked -= Shoot;
            _gun.BoltCocked -= CockBolt;
        }
    }
}
