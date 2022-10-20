using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogData
{
    public string dataID;

    [TextArea(3, 7)]
    public string text;
}

[CreateAssetMenu(menuName = "SO/DialogDatas")]
public class DialogDatas : ScriptableObject
{
    public string ID;
    [SerializeField] private List<DialogData> _textDataList;

    public string FindTextData(string ID)
    {
        string value = "";

        foreach (DialogData td in _textDataList)
        {
            if (td.dataID.CompareTo(ID) == 0)
            {
                value = td.text;
                break;
            }
        }
        return value;
    }
}
