using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunc
{
    public static T RandomEnum<T>()
    {
        System.Array values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range(0, values.Length));
    }
}
