using UnityEngine;

public class Neon : Character
{
    [SerializeField] private float _runningMultiplier;
    [SerializeField] private int _maxEnergy;

    private PlayerController _playerController;
    private float _defaultSpeed;
    private bool _running;
    private float _energy;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _defaultSpeed = _playerController.MoveSpeed;
        Debug.Log("Default speed: " + _defaultSpeed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ability1();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Ability2();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Ability3();
        }

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
    }

    public override void Ability1()
    {
        SwitchRunning();
    }

    public override void Ability2()
    {
        Debug.Log("Ability2");
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
}
