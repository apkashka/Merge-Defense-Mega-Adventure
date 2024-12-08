using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.ToRework.RecyclableScroll
{
    public class RecyclableScroll : MonoBehaviour
    {
        public Action<TableRow, int> RowCreated;

        [SerializeField] private ScrollRect _scroll;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
        [SerializeField] private TableRow _prefab;
        [SerializeField] private int _extraRows = 2;

        private readonly Deque<TableRow> _activeRows = new();
        private readonly Stack<TableRow> _inactiveRows = new();

        private int _offset, _stepHeight, _topLimit, _bottomLimit;
        private int _firstId, _lastId, _count;
        private int _contentWidth;

        private void Awake()
        {
            _scroll.onValueChanged.AddListener(OnScrollChanged);
            _offset = _verticalLayoutGroup.padding.top;
            _stepHeight = (int)_prefab.RectTransform.rect.height + (int)_verticalLayoutGroup.spacing;
            _contentWidth = (int)_scroll.content.sizeDelta.x;
        }

        public void Init(int count)
        {
            foreach (var unit in _activeRows)
            {
                unit.gameObject.SetActive(false);
                _inactiveRows.Push(unit);
            }

            _activeRows.Clear();
            _scroll.content.anchoredPosition = Vector2.zero;
            _scroll.content.sizeDelta = new Vector2(_contentWidth,
                _offset + _stepHeight * count + _verticalLayoutGroup.padding.bottom);
            _scroll.gameObject.SetActive(count > 0);
            _verticalLayoutGroup.padding.top = _offset;

            if (count == 0)
            {
                return;
            }

            _count = count;
            CreateRows();
        }

        private void CreateRows()
        {
            _firstId = 0;
            _bottomLimit = 0;
            _topLimit = _stepHeight;
            var viewportBottom = -(int)_scroll.viewport.rect.height - _stepHeight;
            float position = -_offset;
            var cashedExtra = _extraRows;

            for (int i = 0; i < _count; i++)
            {
                var row = GetUnit();
                RowCreated?.Invoke(row, i);
                row.transform.SetAsLastSibling();
                position -= _stepHeight;
                _lastId = i;

                if (position > viewportBottom) continue;

                if (cashedExtra > 0)
                {
                    cashedExtra--;
                    _topLimit += _stepHeight;
                    continue;
                }

                break;
            }
        }

        private void OnScrollChanged(Vector2 position)
        {
            if (_scroll.content.anchoredPosition.y > _topLimit && _lastId < _count - 1)
            {
                MoveUnit(true);
                return;
            }

            if (_scroll.content.anchoredPosition.y < _bottomLimit && _firstId > 0)
            {
                MoveUnit(false);
            }
        }

        private void MoveUnit(bool toEnd)
        {
            var unitToMove = toEnd ? _activeRows.RemoveFirst() : _activeRows.RemoveLast();
            if (toEnd)
            {
                _activeRows.AddLast(unitToMove);
                unitToMove.transform.SetAsLastSibling();
            }
            else
            {
                _activeRows.AddFirst(unitToMove);
                unitToMove.transform.SetAsFirstSibling();
            }

            _firstId += toEnd ? 1 : -1;
            _lastId += toEnd ? 1 : -1;
            _topLimit += toEnd ? _stepHeight : -_stepHeight;
            _bottomLimit += toEnd ? _stepHeight : -_stepHeight;
            _verticalLayoutGroup.padding.top += toEnd ? _stepHeight : -_stepHeight;

            RowCreated.Invoke(unitToMove, toEnd ? _lastId : _firstId);
        }

        private TableRow GetUnit()
        {
            _inactiveRows.TryPop(out var unit);
            if (unit != null)
            {
                unit.gameObject.SetActive(true);
                _activeRows.AddLast(unit);
                return unit;
            }

            unit = Instantiate(_prefab, _scroll.content);
            _activeRows.AddLast(unit);
            return unit;
        }
    }
}