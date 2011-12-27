using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WindowsInput;
using WindowsInput.Native;

using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace WinPad
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class WinPad : Microsoft.Xna.Framework.Game
	{
		NotifyIcon nIcon;

		private GamePadState currentGamePadState, previousGamePadState;
		private InputSimulator inputSim;
		private double lastScrollTimeVertical, lastScrollTimeHorizontal;
		private Boolean inputDisabled;

		public WinPad()
		{
			inputSim = new InputSimulator();
		}

		protected override void Initialize()
		{
			IsMouseVisible = true;
			inputDisabled = false;

			nIcon = new NotifyIcon();
			nIcon.Text = "WinPad";
			nIcon.Icon = new System.Drawing.Icon("Game.ico", 40, 40);
			nIcon.Visible = true; 

			MenuItem exitItem = new MenuItem();
			exitItem.Index = 0;
			exitItem.Text = "Close";
			exitItem.Click += new System.EventHandler(this.exitItem_Click);
			nIcon.ContextMenu = new ContextMenu();
			nIcon.ContextMenu.MenuItems.Add(exitItem);

			Form f = (Form)Form.FromHandle(Window.Handle);
			f.Shown += new EventHandler(Window_Shown);

			base.Initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			updateGamePadState();

			// Check for enable/disable
			if (currentGamePadState.Buttons.Start == ButtonState.Pressed && currentGamePadState.Buttons.Back == ButtonState.Pressed &&
					currentGamePadState.Buttons.LeftShoulder == ButtonState.Pressed && currentGamePadState.Buttons.RightShoulder == ButtonState.Pressed)
			{
				if (previousGamePadState.Buttons.Start == ButtonState.Released || previousGamePadState.Buttons.Back == ButtonState.Released ||
						previousGamePadState.Buttons.LeftShoulder == ButtonState.Released || previousGamePadState.Buttons.RightShoulder == ButtonState.Released)
				{
					inputDisabled = !inputDisabled;
				}
			}

			if (!inputDisabled)
			{
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

				// Simulate alt tab for the left trigger
				if (currentGamePadState.Triggers.Left > 0.5 && previousGamePadState.Triggers.Left <= 0.5)
				{
					inputSim.Keyboard.KeyDown(VirtualKeyCode.MENU);
					inputSim.Keyboard.KeyPress(VirtualKeyCode.TAB);
				}
				else if (currentGamePadState.Triggers.Left <= 0.5 && previousGamePadState.Triggers.Left > 0.5)
				{
					inputSim.Keyboard.KeyUp(VirtualKeyCode.MENU);
				}

				// Simulate scrolling for right thumbstick
				if (currentGamePadState.ThumbSticks.Right.X != 0)
				{
					if (previousGamePadState.ThumbSticks.Right.X == 0 ||
							gameTime.TotalGameTime.TotalMilliseconds - lastScrollTimeHorizontal > 200 - 160 * Math.Abs(currentGamePadState.ThumbSticks.Right.X))
					{
						inputSim.Mouse.HorizontalScroll(Math.Sign(currentGamePadState.ThumbSticks.Right.X));
						lastScrollTimeHorizontal = gameTime.TotalGameTime.TotalMilliseconds;
					}
				}
				if (currentGamePadState.ThumbSticks.Right.Y != 0)
				{
					if (previousGamePadState.ThumbSticks.Right.Y == 0 ||
							gameTime.TotalGameTime.TotalMilliseconds - lastScrollTimeVertical > 200 - 160 * Math.Abs(currentGamePadState.ThumbSticks.Right.Y))
					{
						inputSim.Mouse.VerticalScroll(Math.Sign(currentGamePadState.ThumbSticks.Right.Y));
						lastScrollTimeVertical = gameTime.TotalGameTime.TotalMilliseconds;
					}
				}
			}

			base.Update(gameTime);
		}

		

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
		}

		private void updateGamePadState()
		{
			previousGamePadState = currentGamePadState;
			currentGamePadState = GamePad.GetState(PlayerIndex.One);
		}

		protected void Window_Shown(object sender, EventArgs e)
		{
			Form f = (Form)Form.FromHandle(Window.Handle);
			f.Hide();
		}

		private void exitItem_Click(object Sender, EventArgs e)
		{
			Exit();
		}
	}
}
