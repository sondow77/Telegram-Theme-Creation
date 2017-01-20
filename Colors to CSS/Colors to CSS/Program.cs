/*
 * Created by SharpDevelop.
 * User: sondo
 * Date: 17/1/2017
 * Time: 5:06 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Colors_to_CSS
{
	class Program
	{
		public static string ConvertHexToRgba(string hex)
		{
			string result = "";
			if (hex.StartsWith("#"))
			{
            	hex = hex.Substring(1);
			}
	        if (hex.Length == 8)
	        {
	        	double alpha = (double)int.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)/255;
	        	result = "rgba(" + int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) + ", " +
	            int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) + ", " +
	        	int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) + ", " +
	        	alpha.ToString().Replace(",",".") + ")";
	        }
	        return result;
		}
		public static void Main(string[] args)
		{
			const string pat = @"[a-zA-Z0-9]+: #(?:[0-9a-fA-F]{2}){3,4};";
			if (args.Length == 2)
	        {
				string colorCode = File.ReadAllText(args[0]);
				Regex r = new Regex(pat, RegexOptions.IgnoreCase);
				Match onlyDec = r.Match(colorCode);
				
				var al = new List<string>();
				while(onlyDec.Success)
				{
					al.Add(onlyDec.Value);
					onlyDec = onlyDec.NextMatch();
				}
				string[] css = al.ToArray();
				for(int i = 0; i < css.Length; i++)
				{
					css[i] = "." + css[i].Replace(":"," :").Replace("Active :",": focus :").Replace("Inactive :",": disabled :").Replace("Ripple :",": active :").Replace("Over :",": hover :").Replace(" :","\r\n{\r\n\tbackground-color :").Replace(";",";\r\n}").Replace(": hover",":hover").Replace(": focus",":focus").Replace(": disabled",":disabled").Replace(": active",":active");
					if(css[i].Contains("Fg"))
					{
						css[i] = css[i].Replace("background-color","color");
					}
					string hex = Regex.Match(css[i],@"#[0-9a-fA-F]{8}",RegexOptions.IgnoreCase).Value;
					css[i] = Regex.Replace(css[i],@"#[0-9a-fA-F]{8}",ConvertHexToRgba(hex));
				}
				
				File.WriteAllLines(args[1],css);
			}
			else
			{
				System.Console.WriteLine("Please enter input and output arguments...");
	            Console.ReadKey(true);
			}
		}
	}
}