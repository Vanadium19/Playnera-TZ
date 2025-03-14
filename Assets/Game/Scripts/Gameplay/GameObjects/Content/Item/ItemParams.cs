using System;
using UnityEngine;

namespace Game.GameObjects.Content
{
    [Serializable]
    public struct ItemParams
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _angle;
        [SerializeField] private Vector2 _size;

        public Vector3 Position => _transform != null ? _transform.position : Vector3.zero;
        public float Angle => _angle;
        public Vector2 Size => _size;
    }
}