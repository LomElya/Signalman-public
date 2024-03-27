using UnityEngine;

public class CameraRay : MonoBehaviour
{
    [SerializeField] private GameObject _ray;

    public void SetActive(bool isActiv) => _ray.SetActive(isActiv);
}
