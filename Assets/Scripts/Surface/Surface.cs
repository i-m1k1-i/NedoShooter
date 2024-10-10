using UnityEngine;

namespace Assets.Scripts.Surface
{
    public class Surface : MonoBehaviour
    {
        [SerializeField] SurfaceType _type;

        public SurfaceType Type => _type;
    }
}
