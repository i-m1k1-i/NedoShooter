using UnityEngine;
using Zenject;

namespace Nedoshooter.Agents
{
    public abstract class Agent : MonoBehaviour
    {
        public IState State { get; private set; }
        public void SetState(IState newState)
        {
            State = newState;
            State.Enter();
        }
        public abstract void Ability1();
        public abstract void Ability2();
        public abstract void Ability3();

        public class Factory : PlaceholderFactory<Agent>
        {
        }
    }
}