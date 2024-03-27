using System;
using TMPro;
using UnityEngine;
using Zenject;

public class InteractedView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textButtom;
    [SerializeField] private TMP_Text _textInteraction;

    private bool _isShow;

    private Camera _camera;

    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }

    private void Start()
    {
        _camera = Camera.main;

        _isShow = false;
        gameObject.SetActive(_isShow);
    }

    public void Show()
    {
        _isShow = true;
        gameObject.SetActive(_isShow);
    }

    public void Hide()
    {
        _isShow = false;
        gameObject.SetActive(_isShow);
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(_camera.transform.position.x, transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}
