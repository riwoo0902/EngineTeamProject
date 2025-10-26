using System;
using System.Collections;
using System.Collections.Generic;

public class ActionSystem : MonoSingleton<ActionSystem>
{
    private List<GameAction> _reactions = null;
    public bool IsPerforming { get; private set; } = false;

    private static Dictionary<Type, List<Action<GameAction>>> _preSubs = new(); //사전 등록

    private static Dictionary<Type, List<Action<GameAction>>> _postSubs = new(); //사후 등록

    private static Dictionary<Type, Func<GameAction, IEnumerator>> _performers = new(); //실행 시 수행할 행동


    public void Perform(GameAction action, System.Action OnPerformFinished = null)
    {
        if (IsPerforming) return; //액션중 중복실행 방지
        IsPerforming = true;

        StartCoroutine(Flow(action, () =>
        {
            IsPerforming = false;
            OnPerformFinished?.Invoke();
        }
        ));
    }

    public void AddReaction(GameAction gameAction)
    {
        _reactions?.Add(gameAction);
    }


    private IEnumerator Flow(GameAction action, Action OnFlowFinished = null)
    {
        _reactions = action.PreReactions; //사전 리액션 실행
        PerformSubscribers(action, _preSubs);
        yield return PerformReactions();

        _reactions = action.PerformReactions; // 리액션 실행
        yield return PerformPerformer(action);
        yield return PerformReactions();

        _reactions = action.PostReactions; //사후 리액션 실행
        PerformSubscribers(action, _postSubs);
        yield return PerformReactions();

        OnFlowFinished?.Invoke(); 
    }

    private IEnumerator PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if (_performers.ContainsKey(type))
        {
            yield return _performers[type](action); //해당 타입의 action 실행
        }
    }

    private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs) 
    {
        //특정 타이밍에 등록된 리액션 실행
        Type type = action.GetType();
        if (subs.ContainsKey(type))
        {
            foreach (var sub in subs[type])
            {
                sub(action);
            }
        }
    }


    private IEnumerator PerformReactions()
    {
        //_reactions 안에 GameAction들 모두 실행
        foreach (var reaction in _reactions)
        {
            yield return Flow(reaction);
        }
    }

    
    public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction
    {
        //Performer등록
        Type type = typeof(T); //타입 가져오기

        IEnumerator wrappedPerformer(GameAction action) => performer((T)action);
        
        //해당 타입이 이미 있으면 덮어쓰기
        if (_performers.ContainsKey(type)) _performers[type] = wrappedPerformer; 
        else _performers.Add(type, wrappedPerformer); //없다면 생성
    }

    public static void DetachPerFormer<T>() where T : GameAction
    {
        Type type = typeof(T); //타입 가져오기
        //해당 타입이 목록에 있으면 Remove
        if (_performers.ContainsKey(type)) _performers.Remove(type);
    }

    public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        //구독중인 함수들을 저장하는 딕셔너리에 작동 타이밍에 따라 저장하기
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? _preSubs : _postSubs;

        void wrappedReaction(GameAction action) => reaction((T)action);
        if (subs.ContainsKey(typeof(T)))
        {
            subs[typeof(T)].Add(wrappedReaction);
        }
        else
        {
            subs.Add(typeof(T), new());
            subs[typeof(T)].Add(wrappedReaction);
        }
    }

    public static void UnsubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T :GameAction
    {
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? _preSubs : _postSubs;
        if (subs.ContainsKey(typeof(T))) //입력 받은 타입의 동작이 있다면 삭제
        {
            void wrappedReaction(GameAction action) => reaction((T)action);
            subs[typeof(T)].Remove(wrappedReaction);
        }
    }
}
