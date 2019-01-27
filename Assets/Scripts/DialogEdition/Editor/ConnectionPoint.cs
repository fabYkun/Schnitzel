using System;
using UnityEngine;

public enum ConnectionPointType { In, Out }

public class ConnectionPoint
{
    public Rect rect;

    public ConnectionPointType type;

    public Node node;

    public GUIStyle style;

    public Action<ConnectionPoint> OnClickConnectionPoint;

    public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint)
    {
        this.node = node;
        this.type = type;
        this.style = style;
        this.OnClickConnectionPoint = OnClickConnectionPoint;
        rect = new Rect(0, 0, 10f, 20f);
    }

    public void Draw()
    {
        float relativePosition = node.outPoints.IndexOf(this);
        if (relativePosition < 0) relativePosition = node.rect.height * 0.5f;
        else relativePosition = (node.rect.height / (node.outPoints.Count + 1)) * (node.outPoints.IndexOf(this) + 1);
        rect.y = node.rect.y + relativePosition - rect.height * 0.5f;

        switch (type)
        {
            case ConnectionPointType.In:
            rect.x = node.rect.x - rect.width + 8f;
            break;

            case ConnectionPointType.Out:
            rect.x = node.rect.x + node.rect.width - 8f;
            break;
        }

        if (GUI.Button(rect, "", style))
        {
            if (OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
    }
}