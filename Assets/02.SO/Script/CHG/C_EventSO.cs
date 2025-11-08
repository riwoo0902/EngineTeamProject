using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct ChoiceReaction
{
    public string ButtonText; //선택버튼 텍스트
    public EventReactionType EffectType; //어떤 Reaction인지
    public int EffectValue; //얼마나 줄지

    public string ChoiceText;
}

public enum EventReactionType 
{
    None, 
    RetouchMaxHealth, //최대체력 수정
    RetouchCurrentHealth, //현재체력 수정
}


[CreateAssetMenu(fileName = "C_EventSO", menuName = "C_SO/C_EventSO")]
public class C_EventSO : ScriptableObject, C_IEventReaction
{
    public string TitleText;
    [TextArea]
    public string StoryText;
    public Sprite EventSprite;

    public List<ChoiceReaction> Choices;

    public void AddListener(int n)
    {
        ChoiceReaction Reaction = Choices[n];

        switch (Reaction.EffectType)
        {
            case EventReactionType.RetouchMaxHealth: 
                EventReaction.Instance.RetouchMaxHealth(Reaction.EffectValue);
                break;
            case EventReactionType.RetouchCurrentHealth:
                EventReaction.Instance.RetouchCurrentHealth(Reaction.EffectValue);
                break;
            case EventReactionType.None:
            default:
                break;
        }
    }
}