using System;
using _01.Script.Lrw.PinBallCompo;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.PinBallEvent
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ChoiceMap : MonoBehaviour
    {
        [SerializeField] private bool Left;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ICanTriggerEvent>(out ICanTriggerEvent iCanTriggerEvent))
            {
                StageManager.Instance.Level++;

                if (Left)
                    MapManager.Instance.OnMapeDir?.Invoke(MapDir.Left);
                else MapManager.Instance.OnMapeDir?.Invoke(MapDir.Right);
            }
        }
    }
}