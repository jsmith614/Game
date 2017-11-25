using GameEngine2017;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Game2017.Animations
{
    public class Player_Jump : Animation
    {
        private float _jumpSpeed = 200.00f;
        private float _maxFrameTime = 0.50f;

        public Player_Jump(bool invertX = false, bool invertY = false) : base()
        {
            InvertX = invertX;
            InvertY = InvertY;
            Initialize();
        }

        public override void Initialize()
        {
            _maxFramesX = 1;
            _maxFramesY = 1;
            _frameTime = _maxFrameTime;
            TextureName = Constants.Player_Idle;
            base.Initialize();
        }

        public override IAnimation Update(float deltaTime, IGameObject gameObject)
        {
            if((_frameTime - deltaTime) <= 0.0f)
            {
                gameObject.Velocity = new Vector2(gameObject.Velocity.X, 0);
                return new Player_Idle(invertX: InvertX);
            }
            var animation = base.Update(deltaTime, gameObject);

            if (_frameTime >= (_maxFrameTime * 0.5f))
            {
                gameObject.Velocity = new Vector2(gameObject.Velocity.X, -_jumpSpeed);
            }
            else
            {
                gameObject.Velocity = new Vector2(gameObject.Velocity.X, _jumpSpeed);
            }

            return animation;
        }

        protected override IAnimation HandleInput(IGameObject gameObject)
        {
            if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.A)
                && InputManager.Instance.KeyboardState.IsKeyDown(Keys.D) == false)
            {
                InvertX = true;
                gameObject.Velocity = new Vector2(-gameObject.MoveSpeed, gameObject.Velocity.Y);
            }
            else if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.A) == false
                && InputManager.Instance.KeyboardState.IsKeyDown(Keys.D))
            {
                InvertX = false;
                gameObject.Velocity = new Vector2(gameObject.MoveSpeed, gameObject.Velocity.Y);
            }
            else
            {
                gameObject.Velocity = new Vector2(0f, gameObject.Velocity.Y);
            }

            return null;
        }
    }
}
