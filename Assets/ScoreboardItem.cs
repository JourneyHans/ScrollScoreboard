using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardItem : MonoBehaviour
{
    // 交替展示的两个数字
    public Text TemplateTxt_1;
    public Text TemplateTxt_2;

    // 显示文本的RectTransform
    private readonly RectTransform[] _txtTransformArray = new RectTransform[2];

    // 用于做移动动画的容器
    private RectTransform _content;
    private Vector2 _itemSize;

    // 当前代表的数字
    private int _curNumber;
    // 目标数字
    private int _desNumber;
    // 切换一个数字需要的时间
    private float _switchTime;
    private float _currentTime;

    // 是否在运动中
    private bool _isInAnimation;
    // 是否以增长的方式（否则为减少）
    private bool _byIncreasing;

    void Awake()
    {
        _content = transform.Find("Content").GetComponent<RectTransform>();
        _txtTransformArray[0] = TemplateTxt_1.GetComponent<RectTransform>();
        _txtTransformArray[1] = TemplateTxt_2.GetComponent<RectTransform>();
        _itemSize = _txtTransformArray[0].sizeDelta;
        _content.sizeDelta = new Vector2(_itemSize.x, _itemSize.y * 2);
        _content.anchorMin = new Vector2(0.5f, 0f);
        _content.anchorMax = new Vector2(0.5f, 0f);
    }

    void Start()
    {

    }

    /// <summary>
    /// 设置数字
    /// </summary>
    /// <param name="number">目标数字</param>
    /// <param name="time">运动时间</param>
    /// <param name="byIncreasing">是否是以增长的方式到达，只在 time != 0 时有效</param>
    public void SetNumber(int number, float time = 0.0f, bool byIncreasing = true)
    {
        if (number == _curNumber)
        {
            // quick return
            return;
        }
        _isInAnimation = false;
        _desNumber = number;
        if (Mathf.Approximately(time, 0.0f))
        {
            _curNumber = _desNumber;
            RefreshNumberText();
            ResetNumberTextPos();
        }
        else
        {
            _byIncreasing = byIncreasing;
            ResetNumberTextPos();
            RefreshNumberText();
            _isInAnimation = true;
            _currentTime = 0f;

            // 是否是减少标志
            int reversFlag = _byIncreasing ? 1 : -1;
            // 计算到目标数字的步长（step），注意如果是减少的话需要用 curNumber - desNumber，这也是设置reversFlag的原因
            int step = (reversFlag * (_desNumber - _curNumber) % 10 + 10) % 10;
            _switchTime = time / step;
        }
    }

    // 刷新显示数字
    private void RefreshNumberText()
    {
        TemplateTxt_1.text = _curNumber.ToString();
        // 确保在0~9之间循环
        int nextNumber = _byIncreasing ? (_curNumber + 1) % 10 : ((_curNumber - 1) % 10 + 10) % 10;
        TemplateTxt_2.text = nextNumber.ToString();
    }

    private void ResetNumberTextPos()
    {
        if (_byIncreasing)
        {
            _content.anchoredPosition = new Vector2(0f, _itemSize.y);
            _txtTransformArray[0].anchoredPosition = new Vector2(0f, -_itemSize.y * 0.5f);
            _txtTransformArray[1].anchoredPosition = new Vector2(0f, _itemSize.y * 0.5f);
        }
        else
        {
            _content.anchoredPosition = new Vector2(0f, 0f);
            _txtTransformArray[0].anchoredPosition = new Vector2(0f, _itemSize.y * 0.5f);
            _txtTransformArray[1].anchoredPosition = new Vector2(0f, -_itemSize.y * 0.5f);
        }
    }

    void Update()
    {
        if (!_isInAnimation)
        {
            return;
        }
        if (_curNumber == _desNumber)
        {
            _isInAnimation = false;
            return;
        }
        _currentTime += Time.deltaTime;
        if (_byIncreasing)
        {
            _content.anchoredPosition = new Vector2(0f, Mathf.Lerp(_itemSize.y, 0, _currentTime / _switchTime));
        }
        else
        {
            _content.anchoredPosition = new Vector2(0f, Mathf.Lerp(0, _itemSize.y, _currentTime / _switchTime));
        }
        if (_currentTime >= _switchTime)
        {
            _currentTime = 0f;
            ResetNumberTextPos();
            _curNumber = _byIncreasing ? (_curNumber + 1) % 10 : ((_curNumber - 1) % 10 + 10) % 10;
            RefreshNumberText();
        }
    }
}
