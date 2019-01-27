using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Node
{
    public Rect rect;
    public string title;
    public bool isDragged;
    public bool isSelected;
    public bool isEntryPoint = false;

    public SODialogBox dialogBox = null;

    public ConnectionPoint inPoint;
    public List<ConnectionPoint> outPoints = new List<ConnectionPoint>();

    public GUIStyle style;
    public GUIStyle entryStyle;
    public GUIStyle defaultNodeStyle;
    public GUIStyle selectedNodeStyle;
    public GUIStyle _outPointStyle;
    public Action<ConnectionPoint> _OnClickOutPoint;
    float initialHeight;

    public Action<Node> OnRemoveNode;

    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle entryNodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
    {
        initialHeight = height;
        rect = new Rect(position.x, position.y, width, initialHeight);
        style = nodeStyle;
        entryStyle = entryNodeStyle;
        inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        _outPointStyle = outPointStyle;
        _OnClickOutPoint = OnClickOutPoint;
        OnRemoveNode = OnClickRemoveNode;
    }

    public void Drag(Vector2 delta)
    {
        rect.position += delta;
        dialogBox.x = rect.position.x;
        dialogBox.y = rect.position.y;
    }

    public void Draw()
    {
        inPoint.Draw();
        if (dialogBox != null && outPoints != null && dialogBox.next != null)
        {
            while (outPoints.Count > dialogBox.next.Length)
                outPoints.RemoveAt(outPoints.Count - 1);
            while (outPoints.Count < dialogBox.next.Length)
                outPoints.Add(new ConnectionPoint(this, ConnectionPointType.Out, _outPointStyle, _OnClickOutPoint));
            rect.height = initialHeight + outPoints.Count * 20;
            foreach (ConnectionPoint connection in outPoints)
            {
                connection.Draw();
            }
        }
        GUI.Box(rect, title, isEntryPoint ? entryStyle : style);
        string label = !string.IsNullOrEmpty(dialogBox.content) ? dialogBox.content.Substring(0, Math.Min(20, dialogBox.content.Length)) : dialogBox.id;
        GUI.Label(rect, label, EditorStyles.centeredGreyMiniLabel);
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.KeyDown:
                if (e.keyCode == KeyCode.Delete)
                {
                    if (isSelected && OnRemoveNode != null)
                    {
                        OnRemoveNode(this);
                        GUI.changed = true;
                    }
                }
                break;
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                        Selection.activeObject = dialogBox;
                    }
                    else if (Event.current.modifiers != EventModifiers.Shift)
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }

                if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Set as entry node"), false, SetAsEntryNode);
        genericMenu.ShowAsContext();
    }

    private void SetAsEntryNode()
    {
        NodeBasedEditor.instance.SetEntryNode(this);
    }

    private void OnClickRemoveNode()
    {
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }
}