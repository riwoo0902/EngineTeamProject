using System;
using System.Collections;
using System.Collections.Generic;

public class ActionSystem : MonoSingleton<ActionSystem>
{
    private List<GameAction> _reactions = null; //현재 실행중인 사전,실행,사후에서 처리할 GameAction을 담는 리스트
    public bool IsPerforming { get; private set; } = false; //현재 다른 Action 실행 여부

    //GameAction을 상속받은 Type의 액션이 실행될 때 사전에 실행될 함수들
    private static Dictionary<Type, List<Action<GameAction>>> _preSubs = new(); 

    //GameAction을 상속받은 Type의 액션이 실행될 때 사후에 실행될 함수들
    private static Dictionary<Type, List<Action<GameAction>>> _postSubs = new();

    //GameAction을 상속받은 Type의 액션이 실행될 때 실행되는 함수들
    private static Dictionary<Type, Func<GameAction, IEnumerator>> _performers = new(); //실행 시 수행할 행동


    //외부에서 액션 실행을 요청하는 시작점
    public void Perform(GameAction action, Action OnPerformFinished = null)
    {
        if (IsPerforming) return; //액션중 중복실행 방지
        IsPerforming = true; 

        //입력받은 GameAction을 Flow로 실행
        StartCoroutine(Flow(action, () =>
        {
            IsPerforming = false;
            OnPerformFinished?.Invoke();
        }
        ));
    }

    //실시간으로 반응 추가
    public void AddReaction(GameAction gameAction)
    {
        _reactions?.Add(gameAction);
    }

    //액션 실행
    private IEnumerator Flow(GameAction action, Action OnFlowFinished = null)
    {
        _reactions = action.PreReactions; //_reactions List를 해당 액션에 미리 정해놓은 사전리액션으로 초기화
        PerformSubscribers(action, _preSubs); //_preSubs에 등록된 사전 구독자를 호출
        yield return PerformReactions(); //_reactions에 담긴 액션 모두 실행

        _reactions = action.PerformReactions; //_reactions List를 action.PerformReactions로 초기화
        yield return PerformPerformer(action); //_performs딕셔너리에서 이 액션 타입에 맞는 핵심
        yield return PerformReactions(); //_reactions에 담긴 부가액션 모두 실행

        _reactions = action.PostReactions; //_reactions List를 해당 액션에 미리 정해놓은 사후리액션으로 초기화
        PerformSubscribers(action, _postSubs);  //_preSubs에 등록된 사후 구독자를 호출
        yield return PerformReactions(); //reactions에 담긴 액션 모두 실행

        OnFlowFinished?.Invoke(); //Perform에서 받은 실행중 표시 False로 바꾸기
    }

    //_performers 딕셔너리에 등록된 맞는 타입의 로직을 실행
    private IEnumerator PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if (_performers.ContainsKey(type))
        {
            yield return _performers[type](action); //해당 타입의 action 실행
        }
    }

        //특정 타이밍에 등록된 리액션 실행
    private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs) 
    {
        Type type = action.GetType();
        if (subs.ContainsKey(type))
        {
            foreach (var sub in subs[type])
            {
                sub(action); //pre나 post딕셔너리에 등록된 함수들 실행
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

    //특정 타입의 실행 로직을 _performers 딕셔너리에 등록
    public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction
    {
        //Performer등록
        Type type = typeof(T); //타입 가져오기

        IEnumerator wrappedPerformer(GameAction action) => performer((T)action); //특정 타입을 받아서 실행하는 함수
        
        //해당 타입이 이미 있으면 덮어쓰기
        if (_performers.ContainsKey(type)) _performers[type] = wrappedPerformer; 
        else _performers.Add(type, wrappedPerformer); //없다면 생성
    }

    //_performers에 저장된 함수 제거
    public static void DetachPerFormer<T>() where T : GameAction
    {
        Type type = typeof(T); //타입 가져오기
        //해당 타입이 목록에 있으면 Remove
        if (_performers.ContainsKey(type)) _performers.Remove(type);
    }

    
    //구독중인 함수들을 저장하는 딕셔너리에 작동 타이밍에 따라 저장하기
    public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? _preSubs : _postSubs;

        void wrappedReaction(GameAction action) => reaction((T)action);
        if (subs.ContainsKey(typeof(T))) //있다면 추가
        {
            subs[typeof(T)].Add(wrappedReaction);
        }
        else //없다면 새로 만들기
        {
            subs.Add(typeof(T), new());
            subs[typeof(T)].Add(wrappedReaction);
        }
    }

    //구독자 제거
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
