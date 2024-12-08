using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeBase.Extra;
using CodeBase.Systems;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class TowerField
    {
        private static string FilePath => Application.dataPath + "Field.bin";
        public List<SpotData> SpotsMatrix { get; private set; }

        public TowerField()
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
            SpotsMatrix = new List<SpotData>();
            for (int i = 0; i < Constants.FieldRows; i++)
            {
                for (int j = 0; j < Constants.FieldColumns; j++)
                {
                    var spot = new SpotData()
                    {
                        X = j, //??
                        Y = i,

                        TowerId = i == 0 || j == 0 || j == Constants.FieldColumns - 1
                            ? -1 //todo blocked in the beginning
                            : 0,
                        Level = 0
                    };
                    SpotsMatrix.Add(spot);
                }
            }

            var spotsWithTowers = new[] { 16, 17, 18 }; //todo to norm
            foreach (var spotId in spotsWithTowers)
            {
                SpotsMatrix[spotId].TowerId = 1;
                SpotsMatrix[spotId].Level = 1;
            }
        }

        public SpotData GetSpot(int id)
        {
            return SpotsMatrix[id];
        }

        public SpotData GetFreeSpot()
        {
            var freeSpots = SpotsMatrix.Where(spot => spot.TowerId == 0).ToArray();
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
            var json = JsonConvert.SerializeObject(SpotsMatrix);
            File.WriteAllText(FilePath, json);
        }

        private void Load()
        {
            var json = File.ReadAllText(FilePath);
            SpotsMatrix = JsonConvert.DeserializeObject<List<SpotData>>(json);
        }

        public SpotData SpotToMerge(int id)
        {
            var selectedSpot = GetSpot(id);
            var mergeSpot = SpotsMatrix.FirstOrDefault(spot =>
                spot.TowerId == selectedSpot.TowerId && spot.Level == selectedSpot.Level);
            return selectedSpot == mergeSpot ? null : mergeSpot;
        }

        public static void DeleteFile()
        {
            File.Delete(FilePath);
        }
    }
}