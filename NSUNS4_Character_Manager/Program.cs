using System;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new Main());
		}
	}
}
