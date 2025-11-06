using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventStageManager : MonoBehaviour
{
    [SerializeField] private List<C_EventSO> Events;
    [SerializeField] private List<TextMeshProUGUI> ButtonText;

    [SerializeField] private List<Button> Buttons;

    [SerializeField] private Image Image;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI StoryText;


    public void Init()
    {
        int r = Random.Range(0, Events.Count);
        C_EventSO _event = Events[r];
        Events.RemoveAt(r);

        int choiceCount = _event.Choices.Count;

        for (int i = 0; i < Buttons.Count; i++)
        {
            if (i < choiceCount)
            {
                ButtonText[i].text = _event.Choices[i].ChoiceText;
                Buttons[i].onClick.RemoveAllListeners();
                Buttons[i].onClick.AddListener(() => _event.AddListener(i));
            }
            else
            {
                Buttons[i].gameObject.SetActive(false);
            }

        }
    }
}
