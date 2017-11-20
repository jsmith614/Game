using GameEngine2017.Interface;
using GameEngine2017.Systems;
using GameEngine2017.Constants;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Game2017.Animations
{
    public class Player_Run : Animation
    {
        public Player_Run(bool invertX = false, bool invertY = false) : base()
        {
            InvertX = invertX;
            InvertY = InvertY;
            Initialize();
        }

        public override void Initialize()
        {
            _maxFramesX = 6;
            _maxFramesY = 1;
            _frameTime = 0.10f;
            TextureName = TextureName.Player_Run;
            base.Initialize();
        }

        public override IAnimation Update(float deltaTime, IGameObject gameObject)
        {
            var animation = base.Update(deltaTime, gameObject);

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

            if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.Space))
            {
                return new Player_Jump(invertX: InvertX);
            }

            if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.W) == false
                && InputManager.Instance.KeyboardState.IsKeyDown(Keys.A) == false
                && InputManager.Instance.KeyboardState.IsKeyDown(Keys.S) == false
                && InputManager.Instance.KeyboardState.IsKeyDown(Keys.D) == false)
            {
                return new Player_Idle(invertX: InvertX);
            }

            return null;
        }
    }
}
