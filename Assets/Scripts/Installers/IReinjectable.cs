using Zenject;

namespace Nedoshooter.Installers
{
    public interface IReinjectable
    {
        void Reinject(DiContainer container);
    }
}