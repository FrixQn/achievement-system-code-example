using DG.Tweening;
using System;
using UnityEngine;

namespace DemoProject.Gameplay
{
    public class Cube : MonoBehaviour
    {
        private const float DESTROY_DURATION = 0.5f;
        private const Ease DESTROY_EASE = Ease.OutBounce;
        private bool _isDestroying;
        [field: SerializeField] public ObjectType Type { get; private set; }

        public event Action<Cube> Clicked;

        private void OnMouseDown()
        {
            if (_isDestroying)
                return;

            Clicked?.Invoke(this);
        }

        public void Destroy()
        {
            transform.DOScale(0f, DESTROY_DURATION).SetEase(DESTROY_EASE).OnComplete(OnShouldDestroy);
        }

        private void OnShouldDestroy()
        {
            Destroy(gameObject);
        }
    }
}
