using System;
using UnityEngine;

[Serializable]
public struct SurfaceStepSound
{
    [SerializeField] private SurfaceType _type;
    [SerializeField] private AudioClip _clip;

    public readonly SurfaceType Type => _type;
    public readonly AudioClip Clip => _clip;
}
