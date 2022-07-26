using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyText : MonoBehaviour
{
    [SerializeField] Text[] txt;
    private void Start()
    {
        for(int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Key)i].ToString();
        }
    }
    private void Update()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Key)i].ToString();
        } 
    }
}
