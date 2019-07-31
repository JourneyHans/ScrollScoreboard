using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public GameObject TemplateItem;     // 实例化的模板
    public int Count = 1;       // 显示几位数字

    // 需要展示的数字，之后将会按“位”分配到每个Item上
    public int Number { set; get; }

    private int _maxNumber;     // 能表现的最高分数
    // 分解后的个位数字，不够的补0
    private List<int> _numberList = new List<int>();
    private List<ScoreboardItem> _itemList = new List<ScoreboardItem>();

    void Awake()
    {
        _maxNumber = 1;
        for (int i = 0; i < Count; i++)
        {
            GameObject itemObj = Instantiate(TemplateItem);
            ScoreboardItem scoreboardItem = itemObj.GetComponent<ScoreboardItem>();
            itemObj.transform.SetParent(transform, false);
            _itemList.Add(scoreboardItem);
            _numberList.Add(0);
            _maxNumber *= 10;
        }
        _maxNumber -= 1;
        TemplateItem.gameObject.SetActive(false);
    }

    /// <summary>
    /// 设置最大数字
    /// </summary>
    public void SetMaxNumber(int maxNumber)
    {
        if (maxNumber > _maxNumber)
        {
            Debug.LogError("Cannot set max number which is over the default one. _maxNumber: " + _maxNumber);
            return;
        }
        _maxNumber = maxNumber;
    }

    /// <summary>
    /// 设置目标数字
    /// </summary>
    public void SetNumber(int number)
    {
        number = Mathf.Clamp(number, 0, _maxNumber);
        bool isIncreasing = number > Number;
        Number = number;

        int unit = Count - 1;   // 位
        while (unit >= 0)
        {
            _numberList[unit--] = number % 10;
            number /= 10;
        }

        for (int i = 0; i < _numberList.Count; i++)
        {
            ScoreboardItem item = _itemList[i];
            item.SetNumber(_numberList[i], 1f, isIncreasing);
        }
    }
}
