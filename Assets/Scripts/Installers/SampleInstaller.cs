using Zenject;
using Assets.Scripts.Economy;
using UnityEngine;
using Assets.Scripts.Agents.Neon;

namespace Assets.Scripts.Installers
{
    public class  SampleInstaller : MonoInstaller
    {
        [SerializeField] private GameObject player;

        public override void InstallBindings()
        {
            Container.Bind<IBalance>().To<PlayerMoney>().FromComponentOn(player).AsSingle();
            Container.Bind<InputReader>().FromNew().AsSingle();
            
        }
    }
}
