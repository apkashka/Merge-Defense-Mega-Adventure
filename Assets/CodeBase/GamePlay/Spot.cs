using CodeBase.Systems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.GamePlay
{
    public class Spot : MonoBehaviour, IPointerClickHandler
    {
        public System.Action<int> Clicked;

        [SerializeField] private MeshRenderer _mesh;

        private int _id;
        private Tower _tower;

        public void Init(int id)
        {
            _id = id;
        }

        public void Highlight(bool on)
        {
            _mesh.material.color = on ? Color.green : UnityEngine.Color.white;
            print($"highlighted: {_id} {on}");
        }

        public void RemoveTower()
        {
            //todo container
            FindFirstObjectByType<TowerSystem>().RemoveTower(_tower);
        }

        public void SetTower(Tower tower)
        {
            _tower = tower;
            _tower.transform.SetParent(transform);
            _tower.transform.localPosition = Vector3.zero;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            print($"id clicked: {_id}");
            Clicked?.Invoke(_id);
        }
    }
}