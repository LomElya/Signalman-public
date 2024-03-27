using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class RecordSlotAnimation : MonoBehaviour
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

    public void Pause()
    {
        _director.Pause();
        SetResult(false);
    }

    public void Resume() => _director.Resume();

    private void SetResult(bool result) => _playAwater.TrySetResult(result);

    private void OnTimelineFinished(PlayableDirector _) => SetResult(true);
}
