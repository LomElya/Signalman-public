using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class MainButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public event Action<MainButton> Click;
    public event Action PointEnter;

    [SerializeField] protected Button _button;
    [SerializeField] protected Image _buttonImage;
    [SerializeField] protected TMP_Text _text;

    [Header("Color")]
    [SerializeField] protected Color _normalColor = Constants.MainColor.NormalColor;
    [SerializeField] protected Color _highlightColor = Constants.MainColor.HighlightColor;
    [SerializeField] protected Color _pressedColor = Constants.MainColor.PressedColor;

    private bool _isOn;

    private int _id;

    public int ID { get => _id; set => _id = value; }

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
        _buttonImage ??= GetComponent<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);

        _buttonImage.color = _normalColor;
        _text.color = _normalColor;
    }

    private void Start()
    {
        _isOn = true;
        Enable();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);

        Disable();
    }

    public void On()
    {
        _isOn = true;
    }

    public void Off()
    {
        _isOn = false;
    }

    public void SetID(int id)
    {
        _text.text = id.ToString();
        ID = id;
    }

    protected void SetColor(Color color)
    {
        _buttonImage.color = color;
        _text.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointEnter();
        PointEnter?.Invoke();

    }
    public void OnPointerExit(PointerEventData eventData) => PoinExit();
    public void OnPointerDown(PointerEventData eventData) => PoinDown();

    public virtual void Select() { }
    public virtual void Unselect() { }

    protected virtual void OnPointEnter() => SetColor(_highlightColor);
    protected virtual void PoinExit() => SetColor(_normalColor);
    protected virtual void PoinDown() => SetColor(_pressedColor);

    protected virtual void OnClick()
    {
        if (!_isOn)
            return;

        Click?.Invoke(this);
    }

    protected virtual void Enable() { }
    protected virtual void Disable() { }
}
