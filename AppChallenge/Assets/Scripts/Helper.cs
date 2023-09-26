using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper {

    public static Color SetColorAlpla( Color color, float alpha ) {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static T[] InitializeArrayWithValue<T>(T value, int length) {
        T[] values = new T[length];
        for (int i = 0; i < length; i++) {
            values[i] = value;
        }
        return values;
    }

    public static float Normalize(float number) {
        return number / Mathf.Abs(number);
    }
}
