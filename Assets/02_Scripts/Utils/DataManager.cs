using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoSingleton<DataManager>
{
    private User user;
    public User User { get { return user; } set { user = value; } }

    private string SAVE_PATH = "";
    private const string SAVE_FILE = "/SaveFile.Json";

    void Awake()
    {
        DontDestroyOnLoad(this);

        SAVE_PATH = Application.dataPath + "/Save";

        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        LoadFromJson();

        InvokeRepeating("SaveUser", 1f, 5f);
    }

    #region Json
    private void LoadFromJson()
    {
        User data;

        if (File.Exists(SAVE_PATH + SAVE_FILE))
        {
            string stringJson = File.ReadAllText(SAVE_PATH + SAVE_FILE);
            data = JsonUtility.FromJson<User>(stringJson);
        }
        else
        {
            data = new User();
        }

        user = data;

        SaveToJson(data);
    }

    public void SaveToJson<T>(T data)
    {
        string stringJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILE, stringJson, System.Text.Encoding.UTF8);
    }
    #endregion

    public void SaveUser()
    {
        SaveToJson(user);
    }

    private void OnApplicationQuit()
    {
        SaveUser();
    }
}
