using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    //Max Stage
    private const int StageLimit = 6;       //Don't change it unless you add a stage
    public int stageLimit => StageLimit;
    public bool isClearGame => maxStage == StageLimit;

    public int stage = 1;
    public int maxStage = 1;
    public KeyCode[] keySetting = new KeyCode[(int)Key.keycount];
    public List<int> selectedBGM = new List<int>();
}