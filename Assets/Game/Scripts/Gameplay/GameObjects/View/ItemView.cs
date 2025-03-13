using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.GameObjects.View
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private float _scaleFactor = 1.2f;
        [SerializeField] private float _duration = 1f;

        private AudioSource _audioSource;
        private Transform _transform;
        private Vector3 _startScale;

        private void Awake()
        {
            _transform = transform;
            _startScale = _transform.localScale;
        }

        [Inject]
        public void Construct(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void PickUp()
        {
            _audioSource.PlayOneShot(_audioClip);

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