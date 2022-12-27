using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    //Max Stage
    private const int StageLimit = 6;       //Don't change it unless you add a stage
    public int stageLimit => StageLimit;
    public bool isClearGame => maxStage == StageLimit;

    private List<int> _selectedBGM = new List<int>();
    public List<int> selectedBGM => _selectedBGM;
    
    public int stage = 1;
    public int maxStage = 1;
    public KeyCode[] keySetting = new KeyCode[(int)Key.keycount];
}