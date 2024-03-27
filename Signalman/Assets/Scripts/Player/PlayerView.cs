using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _moveSparks;
    [SerializeField] private ParticleSystem _jumpSparks;

    private Animator _animator;


    //Sounds
    private AudioSource _audioSource;
    private List<AudioClip> _stepClips;
    private AudioClip _jumpSound;
    private AudioClip _landSound;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetAnimate(string key, float value) => _animator.SetFloat(key, value);
    public void SetAnimate(string key, bool value) => _animator.SetBool(key, value);


    ///Sounds

    public void SetAudioClips(List<AudioClip> stepClips, AudioClip jumpSound, AudioClip landSound)
    {
        _stepClips = stepClips;
        _jumpSound = jumpSound;
        _landSound = landSound;
    }

    public void SterSoundPlay() => _audioSource?.PlayOneShot(_stepClips[RandomRange(_stepClips.Count)]);
    public void StartJumpPlay() => _audioSource?.PlayOneShot(_jumpSound);
    public void StartLandPlay() => _audioSource?.PlayOneShot(_landSound);

    private int RandomRange(int maxCound) =>
        Random.Range(0, maxCound);

    // private void OnDisable() => Unregister();
}
