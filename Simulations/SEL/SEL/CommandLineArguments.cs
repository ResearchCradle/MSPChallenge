﻿using System;
using System.Collections.Generic;
using SEL.API;

namespace SEL
{
	/// <summary>
	/// Utility class which contains information passed in the commandline.
	/// This should allow us to run with commandlines such as "SomeFlag OptionA ValueA FlagB" which toggles SomeFlag and FlagB on and sets OptionA to ValueA
	/// </summary>
	public static class CommandLineArguments
	{
		public const string APIEndpoint = "APIEndpoint";
		public const string MSWPipe = "MSWPipe";

		private static HashSet<string> ms_ValueOptions = new HashSet<string>(); //The options that should have a value associated with them
		private static Dictionary<string, string> ms_OptionValueTable = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

		static CommandLineArguments()
		{
			ms_ValueOptions.Add(APIEndpoint);
			ms_ValueOptions.Add(MSWPipe);

			ParseCommandLine(ms_OptionValueTable);
		}

		private static void ParseCommandLine(Dictionary<string, string> outputTable)
		{
			string[] arguments = Environment.GetCommandLineArgs();

			foreach (string arg in arguments)
			{
				foreach (string optionName in ms_ValueOptions)
				{
					if (arg.StartsWith(optionName) && arg[optionName.Length] == '=')
					{
						outputTable.Add(optionName, arg.Substring(optionName.Length + 1));
						break;
					}
				}
			}
		}

		public static bool HasOptionValue(string optionName)
		{
			string result;
			ms_OptionValueTable.TryGetValue(optionName, out result);
			return result != null;
		}

		public static string GetOptionValue(string optionName)
		{
			string result;
			ms_OptionValueTable.TryGetValue(optionName, out result);
			return result;
		}
	}
}
