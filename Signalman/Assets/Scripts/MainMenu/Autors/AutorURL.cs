using UnityEngine;
using UnityEngine.EventSystems;

public class AutorURL : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string URL;

    public void OnPointerDown(PointerEventData eventData) => Application.OpenURL(URL);
}
