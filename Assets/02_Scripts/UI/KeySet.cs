using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Key
{
    jump,
    attack,
    right,
    left,
    keycount
}
public static class KeySetting { public static Dictionary<Key, KeyCode> keys = new Dictionary<Key, KeyCode>(); }

public class KeySet : MonoBehaviour
{
    int key = -1;
    KeyCode[] defalutkeys = new KeyCode[] { KeyCode.Z, KeyCode.X, KeyCode.D, KeyCode.A };
 
    private void Awake()
    {
        for(int i = 0; i <(int)Key.keycount; i++)
        {
            KeySetting.keys.Add((Key)i, defalutkeys[i]); 
            key = -1;
        }  
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey)
        {
            KeySetting.keys[(Key)key]= keyEvent.keyCode;
            key =  -1;
        }
    }
    public void ChangeKey(int num)
    {
        key = num;
    }
}
