using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameEngine2017
{
    public class InputManager
    {
        private const int _maxInputQueueLength = 7;
        private const float _maxInputQueueTime = 1000.0f;
        private bool _debugMode;
        private float _inputQueueTimer = 0f;
        private MouseState _newMouseState;
        private MouseState _oldMouseState;
        private KeyboardState _newKeyboardState;
        private KeyboardState _oldKeyboardState;
        private GamePadState _newGamepadState_P1;
        private GamePadState _oldGamepadState_P1;
        private Queue<Keys> _inputQueue;
        private SpriteBatch _spriteBatch;
        private static InputManager _instance;
        public bool DebugMode { get { return _debugMode; } }
        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputManager();
                }
                return _instance;
            }
        }

        public MouseState MouseState { get { return _newMouseState; } }
        public KeyboardState KeyboardState { get { return _newKeyboardState; } }
        public float ScrollWheelValue { get; private set; }

        private InputManager()
        {
            _inputQueue = new Queue<Keys>();
            _debugMode = false;
        }

        public void Load(SpriteBatch spriteBatch)
        {
            _inputQueue.Clear();
            _spriteBatch = spriteBatch;
        }

        public void Unload()
        {
            _inputQueue.Clear();
        }

        public void AcquireNewInputStates()
        {
            _newKeyboardState = Keyboard.GetState();
            _newGamepadState_P1 = GamePad.GetState(PlayerIndex.One);
            _newMouseState = Mouse.GetState();
        }

        public void UpdateOldInputStates()
        {
            _oldKeyboardState = _newKeyboardState;
            _oldGamepadState_P1 = _newGamepadState_P1;
            _oldMouseState = _newMouseState;
        }

        public bool Update(float deltaTime)
        {
            // Check for keys to exit game
            if (_newGamepadState_P1.Buttons.Back == ButtonState.Pressed || _newKeyboardState.IsKeyDown(Keys.Escape))
                return false;

            // Queue inputs to some arbitrary number and for some arbitrary time. Idk why I'm doing this but maybe it'll be useful later.
            _inputQueueTimer += deltaTime;
            var pressedKeys = _newKeyboardState.GetPressedKeys();
            foreach (var key in pressedKeys)
            {
                if (CheckKeyPressed(key) && IsAlpha(key))
                {
                    _inputQueue.Enqueue(key);
                    _inputQueueTimer = 0.0f;
                    if (_inputQueue.Count > _maxInputQueueLength)
                    {
                        _inputQueue.Dequeue();
                    }
                }
            }
            if (_inputQueueTimer >= _maxInputQueueTime)
            {
                _inputQueue.Clear();
            }

            // Get difference in scroll values for this frame
            ScrollWheelValue = _newMouseState.ScrollWheelValue - _oldMouseState.ScrollWheelValue;

            // Check for debug mode toggle. Should limit this to debug in vs probably.
            if (CheckKeyPressed(Keys.OemTilde))
            {
                _debugMode = !_debugMode;
            }

            if (CheckKeyPressed(Keys.PageDown))
            {
                MessageHandler.Instance.Dump(new Exception("Manual Dump"));
            }

            return true;
        }

        public bool CheckKeyPressed(Keys Key)
        {
            if (_newKeyboardState.IsKeyDown(Key))
            {
                if (!_oldKeyboardState.IsKeyDown(Key))
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(GameTime gameTime, GameWindow gameWindow, float _deltaTime)
        {

        }

        public bool IsAlpha(Keys key)
        {
            return key >= Keys.A && key <= Keys.Z;
        }

        public bool IsNumeric(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) || (key >= Keys.NumPad0 && key <= Keys.NumPad9);
        }

        public bool IsAlphaNumeric(Keys key)
        {
            return IsAlpha(key) || IsNumeric(key);
        }
    }
}
