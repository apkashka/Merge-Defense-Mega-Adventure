using UnityEngine;

namespace CodeBase
{
    public static class ProgressExtensions
    {
        public static T ToDeserialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string ToJson(this MdmaPlayer obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}