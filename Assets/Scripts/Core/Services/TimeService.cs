using Assets.Scripts.Game.Services;
using DG.Tweening;
using UnityEngine;

namespace playground.Assets.Scripts.Core.Services
{
    public class TimeService :
            Service
    {
        public void StartSlowMotion(float duration, float timeScale)
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, timeScale, 0.85f).OnComplete(() =>
            {
                DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1f, 0.45f).SetDelay(duration);
            });
        }

        public void StopTime()
        {
            SetTimeScale(0f);
        }

        public void StartTime()
        {
            SetTimeScale(1f);
        }

        public void SetTimeScale(float scale)
        {
            Time.timeScale = scale;

        }
    }
}