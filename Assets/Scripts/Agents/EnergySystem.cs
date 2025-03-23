using UnityEngine;

namespace Assets.Scripts.Characters.Neon
{
	public class EnergySystem : MonoBehaviour
	{
        [SerializeField] private NeonEnergyView _energyViewPrefab;
        [SerializeField] private int _maxEnergy = 30;

        [SerializeField] private Neon _neon;
        [SerializeField] private NeonEnergyView _energyView;
		private float _energy;

        public void Prepare(Neon neon)
        {
            _neon = neon;
            Debug.Log("EnergySystem prepare");
        }

        private void Start()
		{
            _neon = FindAnyObjectByType<Neon>();
            _energy = _maxEnergy;
            _energyView = Instantiate(_energyViewPrefab);
            Debug.Log("EnergySystem start");
        }

		private void Update()
		{
            if (_neon.State is NeonRunningState)
            {
                _energy -= Time.deltaTime;
                if (_energy <= 0)
                {
                    _neon.SetState(new NeonNormalState(_neon));
                }
            }
            else if (_energy < _maxEnergy)
            {
                _energy = Mathf.Clamp(_energy + Time.deltaTime, 0, _maxEnergy);
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            _energyView.SetValue(_energy / _maxEnergy);
        }
	}
}