using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardTest : MonoBehaviour
{
    public Scoreboard Scoreboard;
    public InputField InputField;
    public Button BtnApply;
    public Button BtnRandom;
    public Button BtnAdd;
    public Button BtnSub;

    void Awake()
    {
        BtnApply.onClick.AddListener(ApplyNumber);
        BtnRandom.onClick.AddListener(() =>
        {
            int randomNum = Random.Range(0, 10000);
            SetNumber(randomNum);
        });
        BtnAdd.onClick.AddListener(() => { SetNumber(Scoreboard.Number + 1); });
        BtnSub.onClick.AddListener(() => { SetNumber(Scoreboard.Number - 1); });
    }

    private void SetNumber(int number)
    {
        Scoreboard.SetNumber(number);
        InputField.text = number.ToString();
    }

    private void ApplyNumber()
    {
        int inputNumber = 0;
        if (int.TryParse(InputField.text, out inputNumber))
        {
            Scoreboard.SetNumber(inputNumber);
        }
    }
}
