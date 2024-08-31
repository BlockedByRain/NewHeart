using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;   

public class TipsPanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OpenPanel(string name)
    {
        base.OpenPanel(name);
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;
        //½¥ÏÔ
        DOTween.To(()=>canvasGroup.alpha,x=>canvasGroup.alpha=x,1f,1);
    }



    public override void ClosePanel()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        //½¥Òþ
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 1).OnComplete(
            () =>
            {
                base.ClosePanel();
            }
            );
    }


}
