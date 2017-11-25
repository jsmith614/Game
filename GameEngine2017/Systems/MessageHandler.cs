using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Linq;

namespace GameEngine2017
{
    public class MessageHandler
    {
        private float _defaultLifeTime;
        private List<GameMessage> _archive;
        private List<GameMessage> _messages;
        private SpriteBatch _spriteBatch;
        private static MessageHandler _instance;
        public static MessageHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MessageHandler();
                }
                return _instance;
            }
        }

        private MessageHandler()
        {
            _messages = new List<GameMessage>();
            _archive = new List<GameMessage>();
        }

        public void Load(SpriteBatch spriteBatch)
        {
            _defaultLifeTime = Config.Instance.DefaultMessageLifeTime;
            _spriteBatch = spriteBatch;
            ClearMessages();
        }

        public void Unload()
        {
            ClearMessages();
        }

        public void AddMessage(string text, MessageType type = MessageType.Normal)
        {
            _messages.Add(new GameMessage(text, type, _defaultLifeTime));
        }

        public void AddError(string text)
        {
            _messages.Add(new GameMessage(text, MessageType.Error, _defaultLifeTime));
        }

        public void AddMessage(string textFormat, params object[] args)
        {
            var message = string.Format(textFormat, args);
            _messages.Add(new GameMessage(message, MessageType.Normal, _defaultLifeTime));
        }

        public void AddSuccess(string textFormat, params object[] args)
        {
            var message = string.Format(textFormat, args);
            _messages.Add(new GameMessage(message, MessageType.Success, _defaultLifeTime));
        }

        public void AddError(string textFormat, params object[] args)
        {
            var message = string.Format(textFormat, args);
            _messages.Add(new GameMessage(message, MessageType.Error, _defaultLifeTime));
        }

        public void UpdateMessages(float deltaTime)
        {
            _messages.ForEach(m => m.Update(deltaTime));
            _archive.AddRange(_messages.Where(m => m.LifeTime <= 0.0f));
            _messages.RemoveAll(m => m.LifeTime <= 0.0f);
        }

        public void Draw()
        {
            var position = new Vector2(5, 5); // probably change this or have it passed in
            position = Vector2.Transform(position, Matrix.Invert(Camera.Instance.TranslationMatrix));

            _messages.ForEach(m =>
            {

                m.Draw(_spriteBatch, position);
                position.Y += 20;
            });
        }

        public void ClearMessages()
        {
            _archive.Clear();
            _messages.Clear();
        }

        public void Dump(Exception e)
        {
            var folder = @"Error Log";
            var filename = DateTime.Now.ToString().Replace(':', '_').Replace(' ', '_').Replace('/','_');
            var dir = System.IO.Directory.CreateDirectory(folder);
            var fullPath = dir.FullName + @"\" + filename + @".txt";
            var fullPathArchive = dir.FullName + @"\" + filename + @"_Archive.txt";
            // Dump current error and queue'd messages
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath))
            {
                file.WriteLine("Message Queue:");
                file.WriteLine();
                for (var i = 0; i < _messages.Count; ++i)
                {
                    file.WriteLine(_messages[i].Type + ": " + _messages[i].Text);
                    file.WriteLine();
                }
                file.WriteLine();
                file.WriteLine();
                file.WriteLine("Error: ");
                file.WriteLine();
                file.WriteLine(e.Message);
                file.WriteLine();
                file.WriteLine("Source: ");
                file.WriteLine();
                file.WriteLine(e.Source);
                file.WriteLine();
                file.WriteLine("Stack Trace:");
                file.WriteLine();
                file.WriteLine(e.StackTrace);
                file.WriteLine();
            }

            // Dump archived messages
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullPathArchive))
            {
                file.WriteLine("Message Archive:");
                file.WriteLine();
                for (var i = 0; i < _archive.Count; ++i)
                {
                    file.WriteLine(_archive[i].Type + ": " + _archive[i].Text);
                    file.WriteLine();
                }
            }
        }
    }
}
