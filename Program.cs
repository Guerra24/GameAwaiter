using System.Diagnostics;

namespace GameAwaiter
{
	public static class Program
	{

		public static int Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Path not set");
				return -1;
			}

			var app = args[0];
			var delay = args.Length >= 2 ? int.Parse(args[1]) : 5000;

			if (WaitForNewProcess(app))
				return 0;

			Console.WriteLine($"Starting: {app}");

			Thread.Sleep(delay);

			using (var p = new Process())
			{
				p.StartInfo.Verb = "runas";
				p.StartInfo.FileName = app;
				p.StartInfo.WorkingDirectory = Path.GetDirectoryName(app);
				p.StartInfo.UseShellExecute = true;
				p.Start();
				Console.WriteLine($"PID: {p.Id}");
				p.WaitForExit();
			}

			WaitForNewProcess(app);

			return 0;
		}

		private static bool WaitForNewProcess(string app)
		{
			using (var process1 = GetNewProcess(app))
				if (process1 == null)
					return false;
			while (true)
			{
				using (var process2 = GetNewProcess(app))
				{
					if (process2 == null)
						return true;
					Console.WriteLine($"PID: {process2.Id}");
					process2.WaitForExit();
				}
			}
		}

		private static Process? GetNewProcess(string app)
		{
			var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(app));
			if (processes.Length != 1)
			{
				foreach (var p in processes)
					p.Dispose();
				return null;
			}
			return processes[0];
		}
	}
}
