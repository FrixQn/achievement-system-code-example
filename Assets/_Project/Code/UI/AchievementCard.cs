using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AchievementCard : MonoBehaviour
    {
        private const string PROGRESS_TEXT_FORMAT = "{0}/{1}";
        private const float FLASH_COMPLETE_DURATION = 0.3f;
        private const Ease FLASH_COMPLETE_EASE = Ease.InOutBack;
        [field: SerializeField] public string Key { get; private set; }
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _completed;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void UpdateProgress(object currentValue, object requiredValue, float normalizedValue, string format = PROGRESS_TEXT_FORMAT)
        {
            bool isCompleted = normalizedValue == 1f;
            format ??= PROGRESS_TEXT_FORMAT;
            _progressText.text = string.Format(format, currentValue, requiredValue);
            _slider.value = normalizedValue;

            GetCanvasGroup().alpha = isCompleted ? 0.5f : 1f;
            _completed.gameObject.SetActive(normalizedValue == 1f);
            if (isCompleted)
                FlashComplete();
        }

        private void FlashComplete()
        {
            _completed.transform.localScale = Vector3.zero;
            _completed.transform.DOScale(1f, FLASH_COMPLETE_DURATION).SetEase(FLASH_COMPLETE_EASE);
        }

        private CanvasGroup GetCanvasGroup()
        {
            return _canvasGroup == null ? GetComponent<CanvasGroup>() : _canvasGroup;
        }
    }
}
