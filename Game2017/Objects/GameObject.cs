using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameEngine2017;

namespace Game2017
{
    public abstract class GameObject : IGameObject
    {
        private ObjectType _objectType;
        public ObjectType ObjectType { get { return _objectType; } set { Type = value.ToString(); _objectType = value; } }
        public string Type { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Vector2 Scale { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Depth { get; set; }
        public float MoveSpeed { get; set; }
        public string TextureName { get; set; }

        public IAnimation CurrentAnimation { get; set; }

        public virtual void Initialize(Vector2 position, string textureName)
        {
            ObjectType = ObjectType.Object;
            Position = position;
            Velocity = new Vector2();
            Acceleration = new Vector2();// 0, Config.Instance.Gravity);
            Scale = new Vector2(1.0f, 1.0f);
            MoveSpeed = 100.0f;
            Width = 25;
            Height = 25;
            Depth = 1;
            TextureName = textureName;
        }

        public virtual void Load(ContentManager content)
        {
            EventManager.Instance.Subscribe(GameEvent.Test.ToString(), this);
        }

        public virtual void Unload()
        {
            EventManager.Instance.UnsubscribeAll(this);
        }

        public virtual void Update(float deltaTime)
        {
            if (CurrentAnimation != null)
            {
                var nextAnimation = CurrentAnimation.Update(deltaTime, this);
                if (nextAnimation != null)
                {
                    CurrentAnimation = nextAnimation;
                }
            }

            HandleInput(deltaTime);
            
            Position += (Velocity * deltaTime);
        }

        protected virtual void HandleInput(float deltaTime)
        {

        }

        public virtual void Draw(float deltaTime, SpriteBatch spriteBatch)
        {
            Texture2D texture;
            Rectangle sourceRect;
            var spriteEffects = SpriteEffects.None;
            if (CurrentAnimation == null)
            {
                texture = TextureManager.Instance.GetTexture(TextureName);
                sourceRect = new Rectangle(0,0,texture.Width, texture.Height);
            }
            else
            {
                texture = TextureManager.Instance.GetTexture(CurrentAnimation.TextureName);
                sourceRect = CurrentAnimation.GetFrameRect(texture.Width, texture.Height);
                spriteEffects = (CurrentAnimation.InvertX ? SpriteEffects.FlipHorizontally : SpriteEffects.None) 
                    | (CurrentAnimation.InvertY ? SpriteEffects.FlipVertically : SpriteEffects.None);
            }

            var x = (Position.X - (Width * 0.5f));
            var y = (Position.Y - (Height * 0.5f));
            var destRect = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            //, position: new Vector2(x, y)
            //, scale: Scale
            spriteBatch.Draw(texture: texture, layerDepth: Depth, sourceRectangle: sourceRect, destinationRectangle: destRect, effects: spriteEffects);
        }

        public virtual void HandleEvent(string eventName, IGameObject Source)
        {
            // Handle Event
        }
    }
}