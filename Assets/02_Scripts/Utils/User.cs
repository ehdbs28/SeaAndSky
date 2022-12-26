using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int stage = 1;
    public int maxStage = 1;
    public KeyCode[] keySetting = new KeyCode[(int)Key.keycount];
    public List<int> selectedBGM = new List<int>();
}