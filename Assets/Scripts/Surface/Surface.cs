using UnityEngine;

namespace Nedoshooter.Surface
{
    public class Surface : MonoBehaviour
    {
        [SerializeField] SurfaceType _type;

        public SurfaceType Type => _type;
    }
}
