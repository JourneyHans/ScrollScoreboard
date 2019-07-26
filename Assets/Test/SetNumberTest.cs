using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNumberTest : MonoBehaviour
{
    public ScoreboardItem IncressScoreboardItem;
    public ScoreboardItem DecressScoreBoardItem;
    public float Time = 0.0f;

    void Update()
    {
        // check input 0~9
        for (KeyCode i = KeyCode.Alpha0; i <= KeyCode.Alpha9; i++)
        {
            if (Input.GetKeyDown(i))
            {
                IncressScoreboardItem.SetNumber((int) i - 48, Time);
                DecressScoreBoardItem.SetNumber((int) i - 48, Time, false);
            }
        }
    }
}
