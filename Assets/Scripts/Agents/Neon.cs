using Assets.Scripts.Player;
using Assets.Scripts.Weapons;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Agents.Neon
{
    public class Neon : Agent
    {
        [SerializeField] private InputReader _input;

        [SerializeField] private float _runningMultiplier;
        [SerializeField] private float _dashMultiplier;
        [SerializeField] private float _dashDuration;

        private PlayerController _playerController;
        private float _defaultSpeed;
        private int _ability1 = 100;

        public WeaponUser WeaponUser { get; private set; }
        public float DefaultSpeed => _defaultSpeed;
        public float RnningSpeed => _defaultSpeed * _runningMultiplier;
        public InputReader Input => _input;

        
        private void Awake()
        {
            WeaponUser = GetComponent<WeaponUser>();
            _playerController = GetComponent<PlayerController>();
            _defaultSpeed = _playerController.MoveSpeed;
            GameObject obj = new GameObject("EnergySystem");
            EnergySystem energySystem = obj.AddComponent<EnergySystem>();
            SetState(new NeonNormalState(this));
        }

        private void Update()
        {
            State.Update();
        }

        public void SetMoveSpeed(float speed)
        {
            _playerController.SetMoveSpeed(speed);
        }

        public override void Ability1()
        {
            SwitchState();
        }
        private IEnumerator Dash()
        {
            if (_ability1 == 0 || State is NeonNormalState || _playerController.IsGrounded == false)
            {
                yield break;
            }

            Debug.Log("Dashing");
            _playerController.MultiplyMoveDirection(_dashMultiplier);
            _playerController.SetCanMove(false);
            WeaponUser.ChangeWeapon(WeaponType.MainWeapon);

            yield return new WaitForSeconds(_dashDuration);

            SetState(new NeonNormalState(this));
            _playerController.SetCanMove(true);
            Debug.Log("Dashing end");
            _playerController.MultiplyMoveDirection(1);
            _ability1 -= 1;
        }
        private void CallDash()
        {
            StartCoroutine(Dash());
        }

        public override void Ability2()
        {
            Debug.Log("Ability2");
        }

        public override void Ability3()
        {
            Debug.Log("Ability3");
        }

        private void SwitchState()
        {
            if (State is NeonNormalState)
            {
                SetState(new NeonRunningState(this));
            }
            else
            {
                SetState(new NeonNormalState(this));
            }
        }

        private void OnEnable()
        {
            _input.Ability1Event += Ability1;
            _input.Ability2Event += Ability2;
            _input.Ability3Event += Ability3;
            _input.AltFireEvent += CallDash;
        }

        private void OnDisable()
        {
            _input.Ability1Event -= Ability1;
            _input.Ability2Event -= Ability2;
            _input.Ability3Event -= Ability3;
            _input.AltFireEvent -= CallDash;
        }
    }

    public class NeonNormalState : AgentState
    {
        private Neon neon;
        public NeonNormalState(Agent agent) : base(agent) { }

        public override void Enter()
        {
            neon = (Neon)_agent;
            neon.SetMoveSpeed(neon.DefaultSpeed);
            neon.Input.EnableWeaponSwitching();
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            neon.SetState(null);
        }
    }

    public class NeonRunningState : AgentState
    {
        Neon neon;
        public NeonRunningState(Agent agent) : base(agent) { }

        public override void Enter()
        {
            neon = (Neon)_agent;
            neon.SetMoveSpeed(neon.RnningSpeed);
            neon.WeaponUser.ChangeWeapon(WeaponType.Melee);
            neon.Input.DisableWeaponSwitching();
        }
        public override void Update()
        {
            
        }

        public override void Exit()
        {
            neon.SetState(null);
        }
    }
}