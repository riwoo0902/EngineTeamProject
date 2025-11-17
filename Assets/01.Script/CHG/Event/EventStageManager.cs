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

    [Header("PlayerInfo")]
    [SerializeField] private TextMeshProUGUI PlayerHealthText;
    [SerializeField] private TextMeshProUGUI PlayerCoinText;
    [SerializeField] private TextMeshProUGUI PlayerPower;
    [SerializeField] private TextMeshProUGUI LevelText;
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

        TitleText.text = "<fading>" + eventData.TitleText;
        StoryText.text = eventData.StoryText;
        StoryImage.sprite = eventData.EventSprite;

        PlayerInfoSet();
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

    private void PlayerInfoSet()
    {
        PlayerManager pManager = PlayerManager.Instance;
        PlayerHealthText.text = pManager.MaxHealth + "/" + pManager.CurrentHealth;
        PlayerCoinText.text = pManager.Gold.ToString();
        PlayerPower.text = pManager.Power.ToString();
        LevelText.text = StageManager.Instance.Level.ToString();
    }

}
