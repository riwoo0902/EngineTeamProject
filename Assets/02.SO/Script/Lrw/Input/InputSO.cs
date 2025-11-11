using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lrw_Input
{
    [CreateAssetMenu(fileName = "InputSO", menuName = "Input/InputSO")]
    public class InputSO : ScriptableObject, Controler.IPlayerActions
    {
        public Vector2 MousePos { get; private set; }
        public Vector2 MouseScreenPos { get; private set; }
        public Vector2 MoveDir { get; private set; }
        public bool MouseClick { get; private set; }
        
        public event Action OnJumpKeyPress;
        public event Action OnMousePress;
        public event Action OnMouseReleas;
        
        private Controler _controler;
        private Camera _mainCamera;
        private bool _isInitialized;

        public void Initialize()
        {
            if (_isInitialized) return;
            
            _mainCamera = Camera.main;
            
            if (_controler == null)
            {
                _controler = new Controler();
                _controler.Player.SetCallbacks(this);
            }
            
            _controler.Player.Enable();
            _isInitialized = true;
        }

        public void Cleanup()
        {
            if (!_isInitialized) return;
            
            _controler?.Player.Disable();
            
            OnJumpKeyPress = null;
            OnMousePress = null;
            OnMouseReleas = null;
            
            _isInitialized = false;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDir = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnJumpKeyPress?.Invoke();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MouseClick = true;
                OnMousePress?.Invoke();
            }
            else if (context.canceled)
            {
                MouseClick = false;
                OnMouseReleas?.Invoke();
            }
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            MouseScreenPos = context.ReadValue<Vector2>();
            
            if (_mainCamera != null)
            {
                MousePos = _mainCamera.ScreenToWorldPoint(MouseScreenPos);
            }
        }
    }
}

