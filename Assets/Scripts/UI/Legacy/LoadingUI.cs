using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : UIBase
{
    [SerializeField] private RawImage RawImage_LoadingImg;
    [SerializeField] private Slider Slider_LoadingBar;
    [SerializeField] private Image Image_SliderColor;
    [SerializeField] private UnityEngine.Color[] ColorArray_LoadingBar;

    private CancellationTokenSource _cancelToken;
    float[] _pausePoints = { 0.2f, 0.6f, 0.9f };
    int _pauseIndex = 0;

    private void OnEnable()
    {
        LoadAndSetLoadingImg();
    }

    private void OnDisable()
    {
        _cancelToken?.Cancel();
        _cancelToken?.Dispose();
        _cancelToken = null;
    }

    private void LoadAndSetLoadingImg()
    {
        int randomIdx = UnityEngine.Random.Range(0, 2);

        string texturePath = string.Empty;
        switch (randomIdx)
        {
            case 0:
                texturePath = "Texture2D/Texture2D_Loading_1";
                break;
            case 1:
                texturePath = "Texture2D/Texture2D_Loading_2";
                break;
        }

        GameUtil.LoadAndSetTexture(RawImage_LoadingImg, texturePath).Forget();
        StartLoadingResouce(2.7f).Forget();
    }

    private async UniTaskVoid StartLoadingResouce(float duration)
    {
        _cancelToken = new CancellationTokenSource();

        float elapsed = 0f;
        Slider_LoadingBar.value = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / duration);

            if (_pauseIndex < _pausePoints.Length && progress >= _pausePoints[_pauseIndex])
            {
                float pausePointValue = _pausePoints[_pauseIndex];
                Slider_LoadingBar.value = pausePointValue;
                await UniTask.Delay(TimeSpan.FromSeconds(pausePointValue), cancellationToken: _cancelToken.Token);
                _pauseIndex++;
            }

            Slider_LoadingBar.value = progress;
            ChangeColorByLoadingBarValue(progress);

            await UniTask.Yield(PlayerLoopTiming.Update, _cancelToken.Token);
        }

        Slider_LoadingBar.value = 1.0f;
        UIManager.Instance.CloseLoadingUI();
    }

    private void ChangeColorByLoadingBarValue(float curValue)
    {
        if (curValue > 0.8)
        {
            Image_SliderColor.color = ColorArray_LoadingBar.Length >= 4 ? ColorArray_LoadingBar[3] : Color.white;
        }
        else if(curValue > 0.6)
        {
            Image_SliderColor.color = ColorArray_LoadingBar.Length >= 3 ? ColorArray_LoadingBar[2] : Color.white;
        }
        else if (curValue > 0.4)
        {
            Image_SliderColor.color = ColorArray_LoadingBar.Length >= 2 ? ColorArray_LoadingBar[1] : Color.white;
        }
        else
        {
            Image_SliderColor.color = ColorArray_LoadingBar.Length >= 1 ? ColorArray_LoadingBar[0] : Color.white;
        }
    }
}
