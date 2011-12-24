using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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

		public WinPad()
		{
			graphics = new GraphicsDeviceManager(this);
		}

		protected override void Initialize()
		{
			this.IsMouseVisible = true;

			base.Initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			updateGamePad();

			int cursorTranslateX = (int)(gameTime.ElapsedGameTime.Milliseconds * currentGamePadState.ThumbSticks.Left.X);
			int cursorTranslateY = (int)(gameTime.ElapsedGameTime.Milliseconds * currentGamePadState.ThumbSticks.Left.Y);
			Console.Out.WriteLine(cursorTranslateX + ", " + cursorTranslateY);
			Cursor.Position = new System.Drawing.Point(Cursor.Position.X + cursorTranslateX, Cursor.Position.Y - cursorTranslateY);

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
