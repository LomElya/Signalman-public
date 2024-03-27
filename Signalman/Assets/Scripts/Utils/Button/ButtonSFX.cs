using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonSFX : MonoBehaviour
{
    [SerializeField] private MainButton _button;

    [Header("Soudn")]
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _enterSound;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        Subscribe();
    }

    private void OnCLick(MainButton _) => _audioSource.PlayOneShot(_clickSound);
    private void OnPoinEnter() => _audioSource.PlayOneShot(_enterSound);

    private void Subscribe()
    {
        _button.Click += OnCLick;
        _button.PointEnter += OnPoinEnter;
    }

    private void Unsubscribe()
    {
        _button.Click -= OnCLick;
        _button.PointEnter -= OnPoinEnter;
    }

    private void OnDisable() => Unsubscribe();
}
