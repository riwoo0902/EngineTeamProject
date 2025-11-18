using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventStageManager : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private List<Button> Buttons;
    [SerializeField] private GameObject EndButton;

    [SerializeField] private Image StoryImage;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI StoryText;
    
    public void Init(C_EventSO eventData)
    {
        EndButton.SetActive(false);
        int choiceCount = eventData.Choices.Count;

        for (int i = 0; i < Buttons.Count; i++)
        {
            if (i < choiceCount)
            {
                //버튼에 EventSO의 이벤트 적용
                ButtonAddReaction(eventData, i);
            }
            else
            {
                Buttons[i].gameObject.SetActive(false);
            }
        }

        TitleText.text = eventData.TitleText;
        StoryText.text = eventData.StoryText;
        StoryImage.sprite = eventData.EventSprite;

        
    }

    private void ButtonAddReaction(C_EventSO eventData, int i)
    {
        TextMeshProUGUI btnText = Buttons[i].GetComponentInChildren<TextMeshProUGUI>();
        btnText.text = eventData.Choices[i].ButtonText;
        Buttons[i].onClick.RemoveAllListeners();
        Buttons[i].onClick.AddListener(() => eventData.AddListener(i));
        Buttons[i].onClick.AddListener(() =>  ButtonChoice(eventData, i));
    }

    private void ButtonChoice(C_EventSO eventData, int n)
    {
        StoryText.text = eventData.Choices[n].ChoiceText;
        foreach (var item in Buttons)
        {
            item.gameObject.SetActive(false);
            EndButton.SetActive(true);
        }
    }
}
