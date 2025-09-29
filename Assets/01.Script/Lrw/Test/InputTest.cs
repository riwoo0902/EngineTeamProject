using Lrw_Input;
using UnityEngine;

namespace Lrw_Test
{
    public class InputTest : MonoBehaviour
    {
        [SerializeField] private InputSO inputSO;

        private void Update()
        {

            Debug.Log($"x : {inputSO.MousePos.x}\n y : {inputSO.MousePos.y}");
        }

    }
}

