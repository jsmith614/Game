using GameEngine2017.Constants;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine2017.Interface
{
    public interface IAnimation
    {
        bool InvertX { get; set; }
        bool InvertY { get; set; }
        TextureName TextureName { get; set; }
        Dictionary<int, float> KeyFrames { get; set; }

        IAnimation Update(float deltaTime, IGameObject gameObject);
        Rectangle GetFrameRect(int textureWidth, int textureHeight);
    }
}
