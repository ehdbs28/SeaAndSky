using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyText : MonoBehaviour
{
    [SerializeField] Transform textParent;
    private Text[] txt;

    private void Start()
    {
        txt = new Text[(int)Key.keycount];

        for (int i = 0; i < (int)Key.keycount; i++)
        {
            txt[i] = textParent.GetChild(i + 1).GetChild(0).GetComponentInChildren<Text>();
            txt[i].text = KeySetting.keys[(Key)i].ToString();
        }
    }

    private void Update()
    {
        for (int i = 0; i < (int)Key.keycount; i++)
        {
            txt[i].text = KeySetting.keys[(Key)i].ToString();
        }
    }
}
