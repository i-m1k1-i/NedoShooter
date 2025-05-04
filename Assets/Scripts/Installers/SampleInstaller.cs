using Zenject;
using Nedoshooter.Economy;
using UnityEngine;
using Nedoshooter.Players;
using Nedoshooter.WeaponUser;
using Nedoshooter.Agents;

namespace Nedoshooter.Installers
{
    public class  SampleInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _playerPrefab;
        [SerializeField] private Agent _agentPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private InputReader _inputReader;

        public override void InstallBindings()
        {
            Container.Bind<InputReader>().FromNewScriptableObject(_inputReader).AsSingle();

            PlayerController player = Container.InstantiatePrefabForComponent<PlayerController>(_playerPrefab, _spawnPoint.position, Quaternion.identity, null);

            Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(player).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoney>().FromComponentOn(player.gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerWeaponController>().FromComponentOn(player.gameObject).AsSingle();

            Container.Bind<Agent>()
                .FromComponentInNewPrefab(_agentPrefab)
                .UnderTransform(player.transform).AsSingle();
        }
    }
}
