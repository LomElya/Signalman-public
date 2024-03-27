using UnityEngine;

public class SimpleFilter : MonoBehaviour
{
    [SerializeField] private Shader _shader;

    protected Material _material;

    private void Awake() => _material = new(_shader);

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate() { }
}
