using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace playground
{
    public class PressureButton : MonoBehaviour
    {
        public Action OnButtonPress;

        [SerializeField] private Transform _pressurePanel;
        [SerializeField] private TMP_Text _tipText;

        private bool _isPressed;
        private Tween _pressTween;

        public void SetTipText(string text)
        {
            _tipText.text = text;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IInteractWithWorld interactWith))
            {
                _isPressed = true;
                if(_pressTween != null)
                    _pressTween.Kill();
                _pressTween = _pressurePanel.DOLocalMoveY(-0.35f, 0.25f);
                OnButtonPress?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractWithWorld interactWith))
            {
                _isPressed = false;
                if (_pressTween != null)
                    _pressTween.Kill();
                _pressTween = _pressurePanel.DOLocalMoveY(-0.28f, 0.25f);
            }
        }
    }
}
