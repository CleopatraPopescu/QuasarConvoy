﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuasarConvoy.Managers;
using QuasarConvoy.Models;
using QuasarConvoy.Sprites;

namespace QuasarConvoy.Entities
{
    class Player
    {
        #region Sprite Properties&Fields
        public Input Input =
            new Input()
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
                NextShip = Keys.T,
                Shoot = Keys.Space,
                ZoomIN = Keys.O,
                ZoomOUT = Keys.P,
                OpenTrade=Keys.F,
            };
        
        int shipIndex=0;
        
        public Ship ControlledShip { set; get; }

        #endregion

        public Player(List<Ship> convoy)
        {
            if (convoy.Count > 0)
                ControlledShip = convoy[shipIndex];
            else
                ControlledShip = null;
        }

        public void SwitchShip(List<Ship> convoy)
        {
            if(Input.WasPressed(Input.NextShip,Keyboard.GetState())||ControlledShip.IsRemoved)
            {
                bool friendsExist = false;
                for (int i = 0; i < convoy.Count; i++)
                    if (convoy[i].Friendly==true)
                        friendsExist = true;
                if (friendsExist)
                {
                    if (shipIndex >= convoy.Count - 1)
                        shipIndex = 0;
                    else
                        shipIndex++;
                    ControlledShip.IsControlled = false;
                    ControlledShip = convoy[shipIndex];
                }
                else
                    ControlledShip = null;
            }
        }

        #region Old player(ship)
        /*
        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                Velocity.Y -= Speed * (float)Math.Cos(Rotation);
                Velocity.X += Speed * (float)Math.Sin(Rotation);
            }
            if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                Velocity.Y += Speed * (float)Math.Cos(Rotation);
                Velocity.X -= Speed * (float)Math.Sin(Rotation);
            }
            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Rotation-=angSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Rotation += angSpeed;
            }
            if(Keyboard.GetState().IsKeyDown(Input.Reset))
            {
                Position = new Vector2(150, 150);
                Velocity = Vector2.Zero;
                Rotation = 0f;
            }
            if(Keyboard.GetState().IsKeyDown(Input.Reset)&& Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Position = new Vector2(150, 150);
                Velocity = Vector2.Zero;
                Rotation = (float)Math.PI/2;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture,
                    Position,
                    new Rectangle(0, 0, _texture.Width, _texture.Height),
                    Color.White,
                    Rotation,
                    Origin,
                    (float)scale,
                    SpriteEffects.None,
                    0.2f
                    );
        }
        protected void Collide(Sprite sprit)
        {
            if (isTouchingLeft(sprit) || isTouchingRight(sprit))
                Velocity.X = 0;
            if (isTouchingTop(sprit) || isTouchingBot(sprit))
                Velocity.Y = 0;
        }
        protected override void SetAnimations()
        {
            if (Velocity.X > 0)
                _animationManager.Play(_animations["W_Right"]);
            else if (Velocity.X < 0)
                _animationManager.Play(_animations["W_Left"]);
            else
                _animationManager.Play(_animations["W_Front"]);
        }
        private void SpeedLimit()
        {
            if (Velocity.Length() > speedCap)
            {
                Velocity.Normalize();
                Velocity*= speedCap;
            }
            
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            if (isAnimated)
                SetAnimations();
            foreach (Sprite spri in sprites)
                Collide(spri);

            if (isAnimated) base.Update(gameTime, sprites);
            SpeedLimit();
            Position += Velocity;

            //Velocity = Vector2.Zero;
        }*/
        #endregion

        public Vector2 Distance(Vector2 dest)
        {
            return new Vector2(ControlledShip.Position.X - dest.X, ControlledShip.Position.Y - dest.Y);
        }
        public void Update(GameTime gametime, List<Sprite> sprites, List<Ship> convoy)
        {
            if (!ControlledShip.IsControlled)
                ControlledShip.IsControlled = true;
            ControlledShip.MoveControlled(Input);
            ControlledShip.Update(gametime, sprites);
            SwitchShip(convoy);
            Input.Refresh();
        }


    }
}
