using UnityEngine;

namespace Nedoshooter.Weapons
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

        private IFirearm _firearm;
        private AudioSource _gunAudioSource;


        private void Awake()
        {
            _firearm = GetComponent<IFirearm>();
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
            _firearm.Attacked += Shoot;
            _firearm.BoltCocked += CockBolt;
        }

        private void OnDisable()
        {
            _firearm.Attacked -= Shoot;
            _firearm.BoltCocked -= CockBolt;
        }
    }
}
