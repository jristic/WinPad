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

namespace WinPad
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class WinPad : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

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
			Console.Out.WriteLine(gameTime.ElapsedGameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
		}
	}
}
