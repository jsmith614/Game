using GameEngine2017.Interface;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameEngine2017.Systems;
using System;
using GameEngine2017.Constants;

namespace Game2017.Animations
{
    public abstract class Animation : IAnimation
    {
        protected int _currentFrameX;
        protected int _currentFrameY;
        protected int _maxFramesX;
        protected int _maxFramesY;
        protected float _frameTime;
        public bool InvertX { get; set; }
        public bool InvertY { get; set; }
        public TextureName TextureName { get; set; }
        public Dictionary<int, float> KeyFrames { get; set; }

        public virtual void Initialize()
        {
            _currentFrameX = 0;
            _currentFrameY = 0;
            KeyFrames = new Dictionary<int, float>();
            for (var i = 0; i < (_maxFramesX + _maxFramesY); ++i)
            {
                KeyFrames.Add(i, _frameTime);
            }
            _frameTime = KeyFrames[0];

            if(_maxFramesY + _maxFramesX < 2)
            {
                MessageHandler.Instance.AddError("Animation Initialize: max frames not initialized properly.");
            }
        }

        // takes the dt and the invoking object. returns a new animation, if relevant, otherwise returns null if still on the same animation
        public virtual IAnimation Update(float deltaTime, IGameObject gameObject)
        {
            _frameTime -= deltaTime;

            if(_frameTime <= 0.0f)
            {
                // increment x
                ++_currentFrameX;

                // if at the end of x, increment y, set x back to origin
                if(_currentFrameX >= _maxFramesX)
                {
                    _currentFrameX = 0;
                    ++_currentFrameY;
                }

                // if at the end of y, we're done with animation, set both back to origin
                if (_currentFrameY >= _maxFramesY)
                {
                    _currentFrameX = 0;
                    _currentFrameY = 0;
                }

                // set frame timer to timer for new frame
                _frameTime = KeyFrames[(_currentFrameY * _maxFramesX) + _currentFrameX];
            }

            return HandleInput(gameObject);
        }

        public Rectangle GetFrameRect(int textureWidth, int textureHeight)
        {
            var rect = new Rectangle();
            var frameWidth = textureWidth / _maxFramesX;
            var frameHeight = textureHeight / _maxFramesY;
            rect.X = (_currentFrameX * frameWidth);
            rect.Y = (_currentFrameY * frameHeight);
            rect.Width = frameWidth;
            rect.Height = frameHeight;
            return rect;
        }

        protected virtual IAnimation HandleInput(IGameObject gameObject)
        {
            return null;
        }
    }
}
