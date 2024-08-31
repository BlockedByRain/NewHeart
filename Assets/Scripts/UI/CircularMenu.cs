using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;



public class CircularMenu : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
{ 
    public KeyCode keyAction;
    public GameObject Pedestal;

    [SerializeField]
    private CanvasGroup cg;

    [SerializeField]
    private Image _1;

    [SerializeField]
    private Image _2;

    [SerializeField]
    private Image _3;

    [SerializeField]
    private Image _4;

    [SerializeField]
    private Image _5;

    [SerializeField]
    private Image _6;


    private Color hide = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    private Color show = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private bool isShow = false;

    private int currentPart = 0;

    public Action<int> OnClick;


    private void Start()
    {
        ResetCanvas();
        OnClick += (part) =>
        {
            Debug.Log($"当前区域：{part}");
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyAction))
        {
            Show();
        }
    }

    private void ResetCanvas()
    {
        //Pedestal.SetActive(false);
        cg.alpha = 0.0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        ResetColor();
    }

    private void ShowCanvas()
    {
        //Pedestal.SetActive(true);
        cg.alpha = 1.0f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        ResetColor();
    }

    private void ResetColor()
    {
        _1.color = hide;
        _2.color = hide;
        _3.color = hide;
        _4.color = hide;
        _5.color = hide;
        _6.color = hide;
    }


    private void Show()
    {
        isShow = !isShow;
        if (isShow)
        {
            ShowCanvas();
        }
        else
        {
            ResetCanvas();
        }

    }


    public void OnPointerMove(PointerEventData e)
    {
        //不在右上第一块时计算角度返回块数
        //ResetColor();
        int part;
        if (RectTransformUtility.RectangleContainsScreenPoint(_1.rectTransform, e.position))
        {
            part = 1;
        }
        else
        {
            part = ((int)e.position.GetAnlgeFromPoint(new Vector2(Screen.width / 2, Screen.height / 2)) / 60) + 1;
        }
        
        
        if (currentPart != part)
        {
            currentPart = part;
            ResetColor();
            switch (currentPart)
            {
                case 1: _1.color = show; return;
                case 2: _2.color = show; return;
                case 3: _3.color = show; return;
                case 4: _4.color = show; return;
                case 5: _5.color = show; return;
                case 6: _6.color = show; return;
                default: _1.color = show; return;
            }
        }
        else
        {

        }
    }

    public void OnPointerClick(PointerEventData e)
    {
        isShow = false;
        ResetCanvas();
        OnClick?.Invoke(currentPart);
    }

    

    
}
