using UnityEngine;
using System.Collections;

public class CursorDisplay : MonoBehaviour
{
	public Texture2D cursor_neutral;
	public Vector2 hotSpot_neutral = Vector2.zero;
	public Texture2D cursor_grab;
	public Vector2 hotSpot_grab = Vector2.zero;
	public Texture2D cursor_ready_to_grab;
	public Vector2 hotSpot_ready_to_grab = Vector2.zero;
	public CursorMode cursorMode = CursorMode.Auto;


	void update_cursor_neutral()
	{
		Cursor.SetCursor(cursor_neutral, hotSpot_neutral, cursorMode);
	}

	void update_cursor_grab()
	{
		Cursor.SetCursor(cursor_grab, hotSpot_grab, cursorMode);
	}

	void update_cursor_ready_to_grab()
	{
		Cursor.SetCursor(cursor_ready_to_grab, hotSpot_ready_to_grab, cursorMode);
	}
}