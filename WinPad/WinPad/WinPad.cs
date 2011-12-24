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
			updateGamePad();

			// Apply cursor translation
			int cursorTranslateX = (int)(gameTime.ElapsedGameTime.Milliseconds * currentGamePadState.ThumbSticks.Left.X);
			int cursorTranslateY = (int)(gameTime.ElapsedGameTime.Milliseconds * currentGamePadState.ThumbSticks.Left.Y * -1);
			inputSim.Mouse.MoveMouseBy(cursorTranslateX, cursorTranslateY);

			// Simulate LMB-click if A is pressed
			

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
		}

		private void updateGamePad()
		{
			previousGamePadState = currentGamePadState;
			currentGamePadState = GamePad.GetState(PlayerIndex.One);
		}
	}
}
