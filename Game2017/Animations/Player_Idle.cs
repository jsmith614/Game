using GameEngine2017;
using Microsoft.Xna.Framework.Input;

namespace Game2017.Animations
{
    public class Player_Idle : Animation
    {
        public Player_Idle(bool invertX = false, bool invertY = false) : base()
        {
            InvertX = invertX;
            InvertY = InvertY;
            Initialize();
        }

        public override void Initialize()
        {
            _maxFramesX = 1;
            _maxFramesY = 1;
            _frameTime = 0.10f;
            TextureName = Constants.Player_Idle;
            base.Initialize();
        }

        public override IAnimation Update(float deltaTime, IGameObject gameObject)
        {
            var animation = base.Update(deltaTime, gameObject);

            return animation;
        }

        protected override IAnimation HandleInput(IGameObject gameObject)
        {
            if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.Space))
            {
                return new Player_Jump(invertX: InvertX);
            }

            if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.A)
                || InputManager.Instance.KeyboardState.IsKeyDown(Keys.D)
                || InputManager.Instance.KeyboardState.IsKeyDown(Keys.W)
                || InputManager.Instance.KeyboardState.IsKeyDown(Keys.S))
            {
                return new Player_Run(invertX: InputManager.Instance.KeyboardState.IsKeyDown(Keys.A));
            }

            return null;
        }
    }
}
