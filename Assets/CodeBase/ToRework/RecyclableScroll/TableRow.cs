using UnityEngine;

namespace CodeBase.ToRework.RecyclableScroll
{
    public class TableRow : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;
    }
}