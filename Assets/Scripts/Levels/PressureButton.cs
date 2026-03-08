using DG.Tweening;
using playground.Assets.Scripts.Characters;
using System;
using TMPro;
using UnityEngine;

namespace playground.Assets.Scripts.Levels
{
    public class PressureButton :
            MonoBehaviour
    {
        public Action OnButtonPress;

        [SerializeField]
        private Transform pressurePanel;
        [SerializeField]
        private TMP_Text _tipText;

        private bool isPressed;
        private Tween pressTween;

        public void SetTipText(string text)
        {
            _tipText.text = text;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractOwner interactWith))
            {
                isPressed = true;
                if (pressTween != null)
                    pressTween.Kill();
                pressTween = pressurePanel.DOLocalMoveY(-0.35f, 0.25f);
                OnButtonPress?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractOwner interactWith))
            {
                isPressed = false;
                if (pressTween != null)
                    pressTween.Kill();
                pressTween = pressurePanel.DOLocalMoveY(-0.28f, 0.25f);
            }
        }
    }
}