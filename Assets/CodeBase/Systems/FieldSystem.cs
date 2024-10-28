using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeBase.Systems
{
    public class FieldSystem : MonoBehaviour
    {
        
        
        
    }


    public class Field
    {
        private const int Rows = 4;
        private const int Length = 5;

        private readonly List<Spot[]> _spots;
        public Field()
        {
            _spots = new List<Spot[]>();
            for (int i = 0; i < Rows; i++)
            {
                Spot[] spots = new Spot[Length];
                _spots.Add(spots);
            }

            foreach (var spot in _spots[0])
            {
                spot.Id = -1;
            }
        }
        
    }

    public class Spot
    {
        public int Id;
        public int Level;
    }
}