using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debug
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (ArForm game = new ArForm(800, 600, "LearnOpenTK"))
			{
				//Run takes a double, which is how many frames per second it should strive to reach.
				//You can leave that out and it'll just update as fast as the hardware will allow it.
				game.Run(60.0);
			}
		}
	}
}
