using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameEngine2017.Objects;
using GameEngine2017.Systems;
using GameEngine2017.Constants;
using System;
using Microsoft.Xna.Framework.Input;
using GameEngine2017.Interface;

namespace Game2017.Objects
{
    public abstract class GameObject : IGameObject
    {
        public ObjectType Type { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Vector2 Scale { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Depth { get; set; }
        public float MoveSpeed { get; set; }

        public IAnimation CurrentAnimation { get; set; }

        public virtual void Initialize(Vector2 position, int width, int height)
        {
            Type = ObjectType.Object;
            Position = position;
            Velocity = new Vector2();
            Acceleration = new Vector2();// 0, Config.Instance.Gravity);
            Width = width;
            Height = height;
            Scale = new Vector2(1.0f, 1.0f);
            MoveSpeed = 100.0f;
            Depth = 1;
        }

        public virtual void Load(ContentManager content)
        {
            EventManager.Instance.Subscribe(EventName.Test, this);
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
                texture = TextureManager.Instance.GetTexture(Type);
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

        public virtual void HandleEvent(EventName name, IGameObject Source)
        {
            // Handle Event
        }
    }
}