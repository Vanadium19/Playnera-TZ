using System;
using UnityEngine;

namespace Game.GameObjects.Content
{
    [Serializable]
    public struct ShelfParams
    {
        [SerializeField] private float _offsetY;
        [SerializeField] private float _sizeX;

        public float OffsetY => _offsetY;
        public float SizeX => _sizeX;
    }
}