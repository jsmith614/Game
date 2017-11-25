using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine2017
{
    public interface IAnimation
    {
        bool InvertX { get; set; }
        bool InvertY { get; set; }
        string TextureName { get; set; }
        Dictionary<int, float> KeyFrames { get; set; }

        IAnimation Update(float deltaTime, IGameObject gameObject);
        Rectangle GetFrameRect(int textureWidth, int textureHeight);
    }
}
