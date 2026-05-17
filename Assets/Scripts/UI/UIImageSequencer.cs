using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIImageSequencer : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private Sprite[] SpriteArray_Sprite; // 순회할 스프라이트 배열
    [SerializeField] private float _sequenceInterval = 0.1f; // 프레임 간격 (초)
    [SerializeField] private bool _isLoop = true;    // 반복 여부

    private Image _image;
    private CancellationTokenSource _cancelToken;

    private void Awake()
    {
        _image = this.GetComponent<Image>();
    }

    private void OnEnable()
    {
        PlayAnimation().Forget();
    }

    private void OnDisable()
    {
        StopAnimation();
    }

    public async UniTaskVoid PlayAnimation()
    {
        if (SpriteArray_Sprite == null || SpriteArray_Sprite.Length == 0) return;

        StopAnimation();
        _cancelToken = new CancellationTokenSource();

        int currentIndex = 0;

        while (true)
        {
            _image.sprite = SpriteArray_Sprite[currentIndex];

            await UniTask.Delay(TimeSpan.FromSeconds(_sequenceInterval), cancellationToken: _cancelToken.Token);

            currentIndex++;

            if (currentIndex >= SpriteArray_Sprite.Length)
            {
                if (_isLoop)
                {
                    currentIndex = 0;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void StopAnimation()
    {
        if (_cancelToken != null)
        {
            _cancelToken.Cancel();
            _cancelToken.Dispose();
            _cancelToken = null;
        }
    }

    private void OnDestroy()
    {
        StopAnimation();
    }
}
