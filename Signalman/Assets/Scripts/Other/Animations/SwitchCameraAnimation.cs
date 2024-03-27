using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class SwitchCameraAnimation : MonoBehaviour
{
    private PlayableDirector _director;
    private UniTaskCompletionSource<bool> _playAwater;

    private void Awake() => _director = GetComponent<PlayableDirector>();

    public async UniTask Play()
    {

        _playAwater = new UniTaskCompletionSource<bool>();

        _director.stopped -= OnTimelineFinished;
        _director.stopped += OnTimelineFinished;

        _director.Play();

        await _playAwater.Task;
    }

    private void OnTimelineFinished(PlayableDirector _) => _playAwater.TrySetResult(true);
}
