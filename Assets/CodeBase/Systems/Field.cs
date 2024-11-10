using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeBase.Extra;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Systems
{
    public class Field // todo towerField???
    {
        private string FilePath => Application.dataPath + "Field.bin";

        private List<SpotData> _spotsMatrix;
        public List<SpotData> SpotsMatrix => _spotsMatrix;

        public Field()
        {
            if (File.Exists(FilePath))
            {
                Load();
                return;
            }

            CreateNewField();
        }

        private void CreateNewField()
        {
            _spotsMatrix = new List<SpotData>();
            for (int i = 0; i < Constants.FieldRows; i++)
            {
                for (int j = 0; j < Constants.FieldColumns; j++)
                {
                    var spot = new SpotData()
                    {
                        X = j, //??
                        Y = i,
                        TowerId = 0,
                        Level = 0
                    };
                    _spotsMatrix.Add(spot);
                }
            }

            for (int i = 0; i < Constants.FieldColumns; i++)
            {
                _spotsMatrix[i].TowerId = -1;
            }
        }

        public SpotData GetSpot(int id)
        {
            return _spotsMatrix[id];
        }

        public SpotData GetFreeSpot()
        {
            var freeSpots = _spotsMatrix.Where(spot => spot.TowerId == 0).ToArray();
            if (!freeSpots.Any())
            {
                return null;
            }

            var spot = freeSpots[Random.Range(0, freeSpots.Length)];
            spot.TowerId = 1;
            spot.Level = 1; //todo norm
            return spot;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(_spotsMatrix);
            File.WriteAllText(FilePath, json);
        }

        private void Load()
        {
            var json = File.ReadAllText(FilePath);
            _spotsMatrix = JsonConvert.DeserializeObject<List<SpotData>>(json);
        }

        public SpotData SpotToMerge(int id)
        {
            var selectedSpot = GetSpot(id);
            var mergeSpot = _spotsMatrix.FirstOrDefault(spot =>
                spot.TowerId == selectedSpot.TowerId && spot.Level == selectedSpot.Level);
            return selectedSpot == mergeSpot ? null : mergeSpot;
        }
    }
}