using Nedoshooter.Installers;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerHealthView : MonoBehaviour, IReinjectable
{
    [SerializeField] private TextMeshProUGUI _healthTMP;
    [SerializeField] private IHasHealth _health;

    [Inject]
    private void Initialize(IHasHealth health)
    {
        SampleInstaller.Instance.RegisterReinjectable(this);
        _health = health;
    }

    public void Reinject(DiContainer container)
    {
        container.Inject(this);
    }

    private void Update()
    {
        _healthTMP.text = _health.CurrentHealth.ToString();
    }
}
