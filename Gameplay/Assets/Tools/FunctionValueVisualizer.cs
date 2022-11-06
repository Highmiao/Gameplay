using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FunctionValueVisualizer : MonoBehaviour
{
    public Color axisColor = Color.green;
    public Color lineColor = Color.red;

    public float windowSizeX = 200f;
    public float windowSizeY = 200f;

    public float xMaxValue;
    public float yMaxValue;

    public int capacity = 200;

    private Queue<float> m_values = new Queue<float>(200);

    private void OnGUI()
    {
        DrawAxis();
    }

    private void DrawAxis()
    {
        Vector2 origin = new Vector2(2, windowSizeY);
        Vector2 xMax = new Vector2(windowSizeX, windowSizeY);
        Vector2 yMax = new Vector2(2, 0);
        Handles.color = axisColor;
        Handles.DrawLine(origin, xMax);
        Handles.DrawLine(origin, yMax);
    }

    private void DrawValues()
    {
        foreach (var value in m_values)
        {

        }
    }

    #region 接口
    public void AddValue(float _value)
    {
        if (m_values.Count < capacity)
        {
            m_values.Enqueue(_value);
        }
        else
        {
            m_values.Dequeue();
            m_values.Enqueue(_value);
        }
    }
    #endregion
}
