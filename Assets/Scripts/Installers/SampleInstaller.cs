using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Nedoshooter.Economy;
using Nedoshooter.Players;
using Nedoshooter.WeaponUser;
using Nedoshooter.Agents;

namespace Nedoshooter.Installers
{
    public class SampleInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Agent _agentPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private InputReader _inputReader;

        private List<IReinjectable> _reinjectables = new List<IReinjectable>();

        public static SampleInstaller Instance { get; private set; }

        public override void InstallBindings()
        {
            Instance = this;

            Container.Bind<InputReader>().FromInstance(_inputReader).AsSingle();

            Container.BindFactory<Player, Player.Factory>()
                .FromComponentInNewPrefab(_playerPrefab);

            Container.BindFactory<Agent, Agent.Factory>()
                .FromComponentInNewPrefab(_agentPrefab);

            Container.BindInterfacesAndSelfTo<PlayerMoney>().AsSingle();

            Container.Bind<IFirearmed>().FromInstance(null);
            Container.Bind<IHasExtraAmmo>().FromInstance(null);
            Container.Bind<IWeaponSwitcher>().FromInstance(null);

            Container.Bind<PlayerMovement>().FromInstance(null);
            Container.Bind<IMouseLocker>().FromInstance(null);
            Container.Bind<Player>().FromInstance(null);
            Container.Bind<ITarget>().To<Player>().FromInstance(null);
            Container.Bind<IHasHealth>().FromInstance(null);
            Debug.Log("Null binds ended");
        }

        public void Inject(object injectable)
        {
            Container.Inject(injectable);
        }

        private void Awake()
        {
            Player player = Container.Resolve<Player.Factory>()
                .Create();
            player.transform.position = _spawnPoint.position;

            Container.Rebind<Player>()
                .FromInstance(player);
            Container.Rebind<ITarget>().To<Player>()
                .FromInstance(player);

            Debug.Log("PlayerController binded");

            Agent agent = Container.Resolve<Agent.Factory>()
                .Create();
            agent.transform.SetParent(player.transform, false);

            var playerWeaponController = player.GetComponent<PlayerWeaponController>();
            Container.Rebind<IFirearmed>().FromInstance(playerWeaponController);
            Container.Rebind<IHasExtraAmmo>().FromInstance(playerWeaponController);
            Container.Rebind<IWeaponSwitcher>().FromInstance(playerWeaponController);

            var playerMovement = player.GetComponent<PlayerMovement>();
            Container.Rebind<PlayerMovement>().FromInstance(playerMovement);
            Container.Rebind<IMouseLocker>().FromInstance(playerMovement);

            var playerHealth = player.GetComponent<PlayerHealth>();
            Container.Rebind<IHasHealth>().To<PlayerHealth>().FromInstance(playerHealth);

            Reinject();
        }

        public void RegisterReinjectable(IReinjectable reinjectable)
        {
            if (_reinjectables.Contains(reinjectable))
            {
                return;
            }
            _reinjectables.Add(reinjectable);
        }

        private void Reinject()
        {
            foreach (var reinjectable in _reinjectables)
            {
                reinjectable.Reinject(Container);
            }
        }
    }
}
