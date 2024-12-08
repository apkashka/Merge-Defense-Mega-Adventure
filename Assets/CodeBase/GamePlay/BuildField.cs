using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services;
using CodeBase.Systems;
using UnityEngine;

namespace CodeBase.GamePlay
{
    //todo rename
    public class BuildField : MonoBehaviour //todo move in battleGround
    {
        [SerializeField] private List<TowerLine> _towerLines; //todo serialize in rows and columns or change somehow
        [SerializeField] private TowerSystem _towerSystem;

        private int? _id;
        private TowerField _field;
        private List<TowerSpot> _towerSpots;

        public void Init(ProgressService progressService)
        {
            _towerSpots = _towerLines.SelectMany(line => line.Spots).ToList();
            if (_towerSpots.Count != progressService.TowerField.SpotsMatrix.Count)
            {
                Debug.LogError("Wrong spots count");
                return;
            }

            _field = progressService.TowerField;
            
            for (int i = 0; i < _towerSpots.Count; i++)
            {
                var blocked = _field.GetSpot(i).TowerId == -1;
                _towerSpots[i].Init(i, blocked);
                if (blocked)
                {
                    continue;
                }

                _towerSpots[i].Clicked += OnSpotClicked; //todo unsubscribe??
            }

            CreateTowers(_field);
        }

        private void CreateTowers(TowerField towerField)
        {
            foreach (var spot in towerField.SpotsMatrix)
            {
                if (spot.TowerId == -1)
                {
                    //todo locked sell
                    continue;
                }

                if (spot.TowerId == 0)
                {
                    //todo empty/inactive
                    continue;
                }

                SetTower(spot);
            }
        }

        public void CreateNewTower()
        {
            var freeSpot = _field.GetFreeSpot();
            if (freeSpot == null)
            {
                Debug.LogError("No free spots found");
                return; //todo block in UI
            }

            freeSpot.Level = 1;
            SetTower(freeSpot);
        }

        private void SetTower(SpotData spotData)
        {
            var tower = _towerSystem.CreateTower(spotData);
            var i = spotData.i;
            _towerSpots[i].SetTower(tower);
        }

        //todo remake
        private void OnSpotClicked(int id)
        {
            if (_id == null)
            {
                var selectedSpot = _field.GetSpot(id);

                if (selectedSpot.TowerId is -1 or 0)
                {
                    return;
                }

                var spots = _field.SpotsMatrix.Where(spot => spot.Level == selectedSpot.Level).ToList();
                if (spots.Count == 0)
                {
                    return;
                }

                _towerSpots.ForEach(spot => spot.Highlight(false));
                foreach (var spotData in spots)
                {
                    _towerSpots[spotData.i].Highlight(true);
                }

                _id = id;
                _towerSpots[_id.Value].Highlight(false);
                return;
            }

            if (id == _id)
            {
                var spotToMerge = _field.SpotToMerge(id);
                if (spotToMerge != null)
                {
                    _towerSpots[id].RemoveTower();
                    _towerSpots[spotToMerge.i].RemoveTower();
                    spotToMerge.TowerId = 0;
                    spotToMerge.Level = 0;

                    var selectedSpot = _field.GetSpot(id);
                    selectedSpot.Level++;
                    _towerSpots[id].SetTower(_towerSystem.CreateTower(selectedSpot));
                }
            }

            _towerSpots.ForEach(spot => spot.Highlight(false));
            _id = null;
        }
    }
}