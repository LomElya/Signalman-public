using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : UIState
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _speed = 15f;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Image _characterImage;
    [SerializeField] private Sprite[] _spritesCharacter;

    private List<string> _sentence = new();

    private bool _confirmButtonClicked;

    public static UniTask<Disposable<MessagePanel>> Load()
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.LoadDisposable<MessagePanel>(AssetsConstants.MessagePanel);
    }

    public async UniTask<bool> Show(List<string> text)
    {
        _sentence = text;

        await Show();

        var result = await _taskCompletion.Task;

        return result;
    }

    protected override void OnShow()
    {
        base.OnShow();

        StartCoroutine(DisplayTextCoroutine(_sentence));
    }

    /*   public async UniTask<bool> AwaitForDecision(List<string> text)
      {
          Subscribe();

          _taskCompletion = new UniTaskCompletionSource<bool>();

          _canvas.enabled = true;

          StartCoroutine(DisplayTextCoroutine(text));

          var result = await _taskCompletion.Task;

          _canvas.enabled = false;
          Unsubscribe();

          return result;
      } */

    protected override void Subscribe()
    {
        base.Subscribe();

        _nextButton.onClick.AddListener(OnConfirmButtonClick);
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        _nextButton.onClick.RemoveListener(OnConfirmButtonClick);
    }

    private void OnConfirmButtonClick()
    {
        int randomIndex = Random.Range(0, _spritesCharacter.Length);

        _characterImage.sprite = _spritesCharacter[randomIndex];

        _confirmButtonClicked = true;

    }
    public void Stop() => _taskCompletion.TrySetResult(true);

    private IEnumerator DisplayTextCoroutine(List<string> sentences)
    {
        _text.text = string.Empty;

        float delay = 1f / _speed;
        foreach (var sentence in sentences)
        {
            _confirmButtonClicked = false;

            for (int i = 0; i < sentence.Length; i++)
            {
                if (_confirmButtonClicked)
                {
                    _text.text = sentence;
                    _confirmButtonClicked = false;
                    break;
                }

                _text.text += sentence[i];

                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => _confirmButtonClicked == true);
            _text.text = string.Empty;
        }

        Stop();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Stop();
    }
}
