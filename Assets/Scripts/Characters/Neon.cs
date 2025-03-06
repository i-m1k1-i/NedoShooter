using UnityEngine;

public class Neon : Character
{
    [SerializeField] private float _runningMultiplier;
    [SerializeField] private float _dashMultiplier;
    [SerializeField] private int _maxEnergy;
    [SerializeField] private NeonEnergyView _energyView;

    private PlayerController _playerController;
    private float _defaultSpeed;
    private bool _running;
    private float _energy;

    bool _ability1 = true;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _defaultSpeed = _playerController.MoveSpeed;
        Debug.Log("Default speed: " + _defaultSpeed);
        _energy = _maxEnergy;
    }

    private void Update()
    {
        if (_running)
        {
            _energy -= Time.deltaTime;
            if (_energy <= 0)
            {
                SwitchRunning();
            }
        }
        else if (_energy < _maxEnergy)
        {
            _energy = Mathf.Clamp(_energy + Time.deltaTime, 0, _maxEnergy);
        }
        _energyView.SetValue(_energy / _maxEnergy);
    }

    public override void Ability1()
    {
        SwitchRunning();
    }

    public override void Ability2()
    {
        Debug.Log("Ability2");
    }

    public void Dash()
    {
        if (_ability1 == false || _running == false)
        {
            return;
        }

        bool dashed = _playerController.TryMultiplyMoveDirection(_dashMultiplier);
        if (dashed)
        {
            //_ability1 = false;
        }
    }

    public override void Ability3()
    {
        Debug.Log("Ability3");
    }

    private void SwitchRunning()
    {
        _running = !_running;
        UpdateMoveSpeed();
    }

    private void UpdateMoveSpeed()
    {
        if (_running)
        { 
            _playerController.SetMoveSpeed(_defaultSpeed * _runningMultiplier);
        }
        else 
        {
            _playerController.SetMoveSpeed(_defaultSpeed); 
        }
    }

    private void OnEnable()
    {
        _playerController.RightClick += Dash;
    }
}
