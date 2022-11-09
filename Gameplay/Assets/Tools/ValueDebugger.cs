using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ValueDebugger : MonoBehaviour
{
    public Color axisColor = Color.black;
    public Color lineColor = Color.red;

    public float windowSizeX = 200f;
    public float windowSizeY = 200f;

    public float xScale = 1;
    public float yScale = 1;

    public int capacity = 200;

    private float m_originX = 2;
    private List<float> m_values = new List<float>(200);

    public bool pause;

    private void Awake()
    {
        pause = false;
    }

    private void OnValidate()
    {
        if (capacity > windowSizeX)
        {
            capacity = Mathf.FloorToInt(windowSizeX);
        }
    }

    private void OnGUI()
    {
        DrawAxis();
        DrawValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
        }
    }

    private void DrawAxis()
    {
        Vector2 origin = new Vector2(m_originX, windowSizeY);
        Vector2 xMax = new Vector2(windowSizeX, windowSizeY);
        Vector2 yMax = new Vector2(m_originX, 0);
        Handles.color = axisColor;
        Handles.DrawLine(origin, xMax);
        Handles.DrawLine(origin, yMax);
    }

    private void DrawValues()
    {
        Handles.color = lineColor;
        int count = m_values.Count;
        for (int i = 0; i < count - 1; i++)
        {
            var from = m_values[i];
            var to = m_values[i + 1];
            var fromPos = new Vector2(GetX(i), GetY(from));
            var toPos = new Vector2(GetX(i + 1), GetY(to));
            Handles.DrawLine(fromPos, toPos);
        }
    }

    private float GetX(float _value)
    {
        return m_originX + _value * xScale;
    }

    private float GetY(float _value)
    {
        return windowSizeY - _value * yScale;
    }

    #region 接口
    public void AddValue(float _value)
    {
        if (pause) return;

        if (m_values.Count < capacity)
        {
            m_values.Add(_value);
        }
        else
        {
            m_values.RemoveAt(0);
            m_values.Add(_value);
        }
    }
    #endregion
}
