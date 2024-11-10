using System;
using CodeBase.Extra;
using UnityEngine.Serialization;

namespace CodeBase.Systems
{
    [Serializable]
    public class SpotData
    {
        public int X;
        public int Y;
        public int TowerId;
        public int Level;
        
        public int i => X + Y * Constants.FieldColumns;
        //todo remove i or X/Y
    }
}