using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic_5___more_classe

{
    public class Ghost
{
    private List<Texture2D> _textures;
    private Vector2 _speed;
    private Rectangle _location;
    private int _textureIndex; // Allows us to control what texture is drawn
    private SpriteEffects _direction;
    private float _animationSpeed;
    private float _seconds;


        public Rectangle Rect
    {
        get { return _location; }
    }
    public Ghost(List<Texture2D> textures, Rectangle location)
        {
        _textures = textures;
        _textureIndex = 0;
        _speed = Vector2.Zero;
        _location = location;
        _direction = SpriteEffects.None;
        _animationSpeed = 0.2f;
        _seconds = 0;
        }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_textures[0], _location, Color.White);
            spriteBatch.Draw(_textures[0],
            _location,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            _direction,
            1);
            spriteBatch.Draw(_textures[_textureIndex],
            _location,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            _direction,
            1);
        }
        public void Update(MouseState mouseState) 
        {
            if (mouseState.X < _location.X)
            {
                _direction = SpriteEffects.FlipHorizontally;
            }
            else if (mouseState.X > _location.X)
            {
                _direction = SpriteEffects.None;
            }
            _speed = Vector2.Zero;
            if (mouseState.X < _location.X)
            {
                _direction = SpriteEffects.FlipHorizontally;
                _speed.X = -1;
            }
            else if (mouseState.X > _location.X)
            {
                _direction = SpriteEffects.None;
                _speed.X = 1;
            }
            if (mouseState.LeftButton == ButtonState.Released)
            {
                _speed = Vector2.Zero; // Sets speed to zero if mouse not clicked
            }
            _location.Offset(_speed);
        }
        public void Update(GameTime gameTime, MouseState mouseState)
        {
            // Only move if the mouse button is down
            if (mouseState.LeftButton == ButtonState.Released)
            {
                _speed = Vector2.Zero; // Sets speed to zero if mouse not clicked
                _textureIndex = 0;
                _seconds = 0f;

                if (_speed != Vector2.Zero) // Ghost is moving
                {
                    _seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_seconds > _animationSpeed)
                    {
                        _seconds = 0;
                        _textureIndex++;
                        if (_textureIndex >= _textures.Count)
                            _textureIndex = 1;

                    }
                }
                _location.Offset(_speed);
            }
        }
    }


}
