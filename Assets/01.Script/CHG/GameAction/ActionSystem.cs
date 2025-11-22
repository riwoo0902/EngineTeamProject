using System;
using System.Collections;
using System.Collections.Generic;

public class ActionSystem : MonoSingleton<ActionSystem>
{
    private List<GameAction> _reactions = null; //���� �������� ����,����,���Ŀ��� ó���� GameAction�� ��� ����Ʈ
    public bool IsPerforming { get; private set; } = false; //���� �ٸ� Action ���� ����

    //GameAction�� ��ӹ��� Type�� �׼��� ����� �� ������ ����� �Լ���
    private static Dictionary<Type, List<Action<GameAction>>> _preSubs = new(); 

    //GameAction�� ��ӹ��� Type�� �׼��� ����� �� ���Ŀ� ����� �Լ���
    private static Dictionary<Type, List<Action<GameAction>>> _postSubs = new();

    //GameAction�� ��ӹ��� Type�� �׼��� ����� �� ����Ǵ� �Լ���
    private static Dictionary<Type, Func<GameAction, IEnumerator>> _performers = new(); //���� �� ������ �ൿ

    protected override void Awake()
    {
        base.Awake();
    }

    //�ܺο��� �׼� ������ ��û
    public void Perform(GameAction action, Action OnPerformFinished = null)
    {
        if (IsPerforming) return; //�׼��� �ߺ����� ����
        IsPerforming = true; 

        //�Է¹��� GameAction�� Flow�� ����
        StartCoroutine(Flow(action, () =>
        {
            IsPerforming = false;
            OnPerformFinished?.Invoke();
        }
        ));
    }

    //�ǽð����� ���� �߰�
    public void AddReaction(GameAction gameAction)
    {
        _reactions?.Add(gameAction);
    }

    //�׼� ����
    private IEnumerator Flow(GameAction action, Action OnFlowFinished = null)
    {
        _reactions = action.PreReactions; //_reactions List�� �ش� �׼ǿ� �̸� ���س��� �������׼����� �ʱ�ȭ
        PerformSubscribers(action, _preSubs); //_preSubs�� ��ϵ� ���� �����ڸ� ȣ��
        yield return PerformReactions(); //_reactions�� ��� �׼� ��� ����

        _reactions = action.PerformReactions; //_reactions List�� action.PerformReactions�� �ʱ�ȭ
        yield return PerformPerformer(action); //_performs��ųʸ����� �� �׼� Ÿ�Կ� �´� �ٽ�
        yield return PerformReactions(); //_reactions�� ��� �ΰ��׼� ��� ����

        _reactions = action.PostReactions; //_reactions List�� �ش� �׼ǿ� �̸� ���س��� ���ĸ��׼����� �ʱ�ȭ
        PerformSubscribers(action, _postSubs);  //_preSubs�� ��ϵ� ���� �����ڸ� ȣ��
        yield return PerformReactions(); //reactions�� ��� �׼� ��� ����

        OnFlowFinished?.Invoke(); //Perform���� ���� ������ ǥ�� False�� �ٲٱ�
    }

    //_performers ��ųʸ��� ��ϵ� �´� Ÿ���� �ڵ� ����
    private IEnumerator PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if (_performers.ContainsKey(type))
        {
            yield return _performers[type](action); //�ش� Ÿ���� action ����
        }
    }

        //Ư�� Ÿ�ֿ̹� ��ϵ� ���׼� ����
    private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs) 
    {
        Type type = action.GetType();
        if (subs.ContainsKey(type))
        {
            foreach (var sub in subs[type])
            {
                sub(action); //pre�� post��ųʸ��� ��ϵ� �Լ��� ����
            }
        }
    }


    private IEnumerator PerformReactions()
    {
        //_reactions �ȿ� GameAction�� ��� ����
        foreach (var reaction in _reactions)
        {
            yield return Flow(reaction);
        }
    }

    //Ư�� Ÿ���� ���� ������ _performers ��ųʸ��� ���
    public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction
    {
        //Performer���
        Type type = typeof(T); //Ÿ�� ��������

        IEnumerator wrappedPerformer(GameAction action) => performer((T)action); //Ư�� Ÿ���� �޾Ƽ� �����ϴ� �Լ�
        
        //�ش� Ÿ���� �̹� ������ �����
        if (_performers.ContainsKey(type)) _performers[type] = wrappedPerformer; 
        else _performers.Add(type, wrappedPerformer); //���ٸ� ����
    }

    //_performers�� ����� �Լ� ����
    public static void DetachPerFormer<T>() where T : GameAction
    {
        Type type = typeof(T); //Ÿ�� ��������
        //�ش� Ÿ���� ��Ͽ� ������ Remove
        if (_performers.ContainsKey(type)) _performers.Remove(type);
    }

    
    //�������� �Լ����� �����ϴ� ��ųʸ��� �۵� Ÿ�ֿ̹� ���� �����ϱ�
    public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? _preSubs : _postSubs;

        void wrappedReaction(GameAction action) => reaction((T)action);
        if (subs.ContainsKey(typeof(T))) //�ִٸ� �߰�
        {
            subs[typeof(T)].Add(wrappedReaction);
        }
        else //���ٸ� ���� �����
        {
            subs.Add(typeof(T), new());
            subs[typeof(T)].Add(wrappedReaction);
        }
    }

    //������ ����
    public static void UnsubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T :GameAction
    {
        
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? _preSubs : _postSubs;
        if (subs.ContainsKey(typeof(T))) //�Է� ���� Ÿ���� ������ �ִٸ� ����
        {
            void wrappedReaction(GameAction action) => reaction((T)action);
            subs[typeof(T)].Remove(wrappedReaction);
            
        }
    }
}
