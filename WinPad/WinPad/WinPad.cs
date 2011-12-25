using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using WindowsInput;
using WindowsInput.Native;

namespace WinPad
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class WinPad : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private GamePadState currentGamePadState, previousGamePadState;

		InputSimulator inputSim;

		public WinPad()
		{
			graphics = new GraphicsDeviceManager(this);
			inputSim = new InputSimulator();
		}

		protected override void Initialize()
		{
			this.IsMouseVisible = true;

			graphics.PreferredBackBufferWidth = 300;
			graphics.PreferredBackBufferHeight = 200;
			graphics.ApplyChanges();

			base.Initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			updateGamePadState();


			// Apply cursor translation 
			int cursorTranslateX = (int)(gameTime.ElapsedGameTime.Milliseconds * currentGamePadState.ThumbSticks.Left.X);
			int cursorTranslateY = (int)(gameTime.ElapsedGameTime.Milliseconds * currentGamePadState.ThumbSticks.Left.Y * -1);
			inputSim.Mouse.MoveMouseBy(cursorTranslateX, cursorTranslateY);


			// Simulate LMB-click if A is pressed
			if (currentGamePadState.Buttons.A == ButtonState.Pressed && previousGamePadState.Buttons.A == ButtonState.Released)
			{
				inputSim.Mouse.LeftButtonDown();
			}
			else if (currentGamePadState.Buttons.A == ButtonState.Released && previousGamePadState.Buttons.A == ButtonState.Pressed)
			{
				inputSim.Mouse.LeftButtonUp();
			}

			// Simulate RMB-click if X is pressed
			if (currentGamePadState.Buttons.X == ButtonState.Pressed && previousGamePadState.Buttons.X == ButtonState.Released)
			{
				inputSim.Mouse.RightButtonDown();
			}
			else if (currentGamePadState.Buttons.X == ButtonState.Released && previousGamePadState.Buttons.X == ButtonState.Pressed)
			{
				inputSim.Mouse.RightButtonUp();
			}
			

			// Simulate arrow keys for the d-pad 
			if (currentGamePadState.DPad.Up == ButtonState.Pressed && previousGamePadState.DPad.Up == ButtonState.Released)
			{
				inputSim.Keyboard.KeyPress(VirtualKeyCode.UP);
			}
			if (currentGamePadState.DPad.Down == ButtonState.Pressed && previousGamePadState.DPad.Down == ButtonState.Released)
			{
				inputSim.Keyboard.KeyPress(VirtualKeyCode.DOWN);
			}
			if (currentGamePadState.DPad.Left == ButtonState.Pressed && previousGamePadState.DPad.Left == ButtonState.Released)
			{
				inputSim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
			}
			if (currentGamePadState.DPad.Right == ButtonState.Pressed && previousGamePadState.DPad.Right == ButtonState.Released)
			{
				inputSim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
		}

		private void updateGamePadState()
		{
			previousGamePadState = currentGamePadState;
			currentGamePadState = GamePad.GetState(PlayerIndex.One);
		}
	}
}
