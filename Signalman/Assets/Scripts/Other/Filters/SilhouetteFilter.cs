using UnityEngine;

public class SilhouetteFilter : SimpleFilter
{
    [SerializeField] private Color _farColor;

    protected override void OnUpdate() => _material.SetColor("_FarColor", _farColor);
}
