using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Texture2D[] cursors;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(instance);
        }
    }
    public static void SetCursors(ObjectType objectType) 
    {
        if (instance == null)
            return;
        Cursor.SetCursor(instance.cursors[(int)objectType], Vector2.zero, CursorMode.Auto);
    }
}
