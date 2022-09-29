using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Key
{
    jump,
    attack,
    right,
    left,
    down,
    changeworld,
    pullBox,
    keycount
}
public static class KeySetting { public static Dictionary<Key, KeyCode> keys = new Dictionary<Key, KeyCode>(); }

public class KeySet : MonoBehaviour
{
    int key = -1;
    KeyCode[] defalutkeys = new KeyCode[] { KeyCode.X, KeyCode.Z, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.Space, KeyCode.LeftShift };
    string[] keyNames = new string[] { "Jump", "Attack", "Right", "Left", "Down", "Change World", "Pull Box" };

    [SerializeField] private GameObject keySettingPanel;

    private void Awake()
    {
        if (KeySetting.keys.Count == 0)
        {
            if (DataManager.Instance.User.keySetting[0] == KeyCode.None)
            {
                for (int i = 0; i < (int)Key.keycount; i++)
                {
                    KeySetting.keys.Add((Key)i, defalutkeys[i]);
                    DataManager.Instance.User.keySetting[i] = defalutkeys[i];
                }
            }
            else
            {
                for (int i = 0; i < (int)Key.keycount; i++)
                {
                    KeySetting.keys.Add((Key)i, DataManager.Instance.User.keySetting[i]);
                }
            }
        }

        for (int i = 0; i < defalutkeys.Length; i++)
        {
            GameObject panel = Instantiate(keySettingPanel, keySettingPanel.transform.parent);
            panel.transform.GetChild(1).GetComponent<Text>().text = keyNames[i];
        }

        keySettingPanel.SetActive(false);
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey && key != -1)
        {
            KeySetting.keys[(Key)key] = keyEvent.keyCode;
            DataManager.Instance.User.keySetting[key] = keyEvent.keyCode;
            key = -1;
        }
    }

    public void ChangeKey(int num)
    {
        key = num;
    }

    public void ChangeKey(GameObject callObj)
    {
        key = callObj.transform.GetSiblingIndex() - 1;
    }
}
