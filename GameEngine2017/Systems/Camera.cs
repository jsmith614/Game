using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine2017.Systems
{
    public class Camera
    {
        private const float _scrollSpeed = 0.02f;
        private const float _rotateSpeed = 1.00f;
        private const float _moveSpeed = 100.00f;
        private static Camera _instance;
        public static Camera Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Camera();
                }
                return _instance;
            }
        }

        public GameWindow Window { get; set; }
        public Matrix TranslationMatrix {
            get
            {
                return Matrix.CreateTranslation(-(int)_position.X, -(int)_position.Y, 0) * 
                    Matrix.CreateRotationZ(Rotation) * 
                    Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) * 
                    Matrix.CreateTranslation(new Vector3(Center, 0));
            }
        }
        private Vector2 _position;
        public Vector2 Position { get { return _position; } set { _position = value; } }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; } // radians
        public Vector2 Center
        {
            get
            {
                return new Vector2(Width * 0.5f, Height * 0.5f);
            }
        }

        private Camera()
        {
            Zoom = 1.0f;
            _position = new Vector2(0, 0);
        }

        public void Initialize(GameWindow window)
        {
            Window = window;
        }

        public void Load(GameWindow window)
        {
            Window = window;
            Width = window.ClientBounds.Width;
            Height = window.ClientBounds.Height;
        }

        public void Unload()
        {

        }

        public void Update(float deltaTime)
        {
            HandleInput(deltaTime);
        }

        private void HandleInput(float deltaTime)
        {
            var input = InputManager.Instance;

            Zoom += (input.ScrollWheelValue * _scrollSpeed) * deltaTime;
            if(Zoom < 0.1f)
            {
                Zoom = 0.1f;
            }
            else if (Zoom > 10.0f)
            {
                Zoom = 10.0f;
            }

            if (input.KeyboardState.IsKeyDown(Keys.E))
            {
                Rotation += _rotateSpeed * deltaTime;
            }
            if (input.KeyboardState.IsKeyDown(Keys.Q))
            {
                Rotation -= _rotateSpeed * deltaTime;
            }

            // Will probably change this to follow the player later? depending on the game idk.
            //if (input.KeyboardState.IsKeyDown(Keys.A))
            //{
            //    _position.X -= (_moveSpeed) * deltaTime;
            //}
            //if (input.KeyboardState.IsKeyDown(Keys.D))
            //{
            //    _position.X += (_moveSpeed) * deltaTime;
            //}
            //if (input.KeyboardState.IsKeyDown(Keys.W))
            //{
            //    _position.Y -= (_moveSpeed) * deltaTime;
            //}
            //if (input.KeyboardState.IsKeyDown(Keys.S))
            //{
            //    _position.Y += (_moveSpeed) * deltaTime;
            //}
        }
    }
}
