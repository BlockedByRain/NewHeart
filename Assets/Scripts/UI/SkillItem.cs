using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public static Color NormalColor = Color.white;
    [SerializeField]
    public static Color SelectColor = new Color(0.4f,0.75f,1f,1f);

    public Image SkillImage;
    [SerializeField]
    public static Vector3 SelectMove = new Vector3(0, 40, 0);

    private bool isSelect;

    public bool IsSelect { get => isSelect;
        set
        {
            isSelect = value;
            if (isSelect)
            {
                SkillImage.transform.localPosition = SelectMove;
            }
            else
            {
                SkillImage.transform.localPosition = Vector3.zero;
                
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillImage.color = NormalColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SkillImage.color = SelectColor;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsSelect = true;
    }
}
