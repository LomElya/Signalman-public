using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TerminalSFX : MonoBehaviour
{
    [SerializeField] private TerminalInteractableZoneV2 _interactableZone;

    [Header("Soudn")]
    [SerializeField] private AudioClip _interactSound;
    [SerializeField] private AudioClip _exitSound;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        Subscribe();
    }

    private void OnOnteracted() => _audioSource.PlayOneShot(_interactSound);

    private void OnExit() => _audioSource.PlayOneShot(_exitSound);

    private void Subscribe()
    {
        _interactableZone.Interacted += OnOnteracted;
        _interactableZone.OnExit += OnExit;
    }

    private void Unsubscribe()
    {
        _interactableZone.Interacted -= OnOnteracted;
        _interactableZone.OnExit -= OnExit;
    }

    private void OnDisable() => Unsubscribe();
}
