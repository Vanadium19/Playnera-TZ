using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.GameObjects.View
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private float _scaleFactor = 1.2f;
        [SerializeField] private float _duration = 1f;

        private Transform _transform;
        private Vector3 _startScale;

        // private Tween _scaleTween;

        private void Awake()
        {
            _transform = transform;
            _startScale = _transform.localScale;
        }

        public void PickUp()
        {
            _transform.DOScale(_scaleFactor, _duration)
                .SetEase(Ease.InOutSine);
        }

        public void Drop()
        {
            _transform.DOScale(_startScale, _duration)
                .SetEase(Ease.InOutSine);
        }
    }
}