/*
 * Created by SharpDevelop.
 * User: sondo
 * Date: 18/1/2017
 * Time: 4:48 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace CSS_to_Colors
{
	class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length == 2)
	        {
				string cssCode = File.ReadAllText(args[0]);
				string cCode = cssCode.Replace(".","").Replace("\r\n{\r\n\t","").Replace("background-","").Replace("color","").Replace(" : ",": ").Replace(":hover","Over").Replace(":focus","Active").Replace(":disabled","Inactive").Replace(":active","Ripple").Replace("\r\n}","");
				string[] stringSeparators = new string[] { "\r\n" };
				string[] cssA = cCode.Split(stringSeparators,StringSplitOptions.None);
				for(int i = 0; i < cssA.Length;i++)
				{
					//rgba\((\d{1,3},\s*){3}\d{1,3}\)
					string rgba = Regex.Match(cssA[i],@"rgba\((\d{1,3},\s*){3}\d{1}.{0,1}\d+\)",RegexOptions.IgnoreCase).Value;
					cssA[i] = Regex.Replace(cssA[i],@"rgba\((\d{1,3},\s*){3}\d{1}.{0,1}\d+\)",ConvertRgbaToHex(rgba),RegexOptions.IgnoreCase);
				}
				File.WriteAllLines(args[1],cssA);
			}
			else
			{
				System.Console.WriteLine("Please enter input and output arguments...");
	            Console.ReadKey(true);
			}
		}
		public static string ConvertRgbaToHex(string rgba)
		{
			string result = "";
			if (Regex.IsMatch(rgba, @"rgba\((\d{1,3},\s*){3}\d{1}.{0,1}\d+\)",RegexOptions.IgnoreCase))
			{
				result = "#";
				MatchCollection cols = Regex.Matches(rgba,@"\d+(\.{0,1})(\d+|)",RegexOptions.IgnoreCase);
				decimal a = Math.Round(decimal.Parse(cols[3].Value.Insert(1,","),NumberStyles.AllowDecimalPoint) * 255);
				result += DecToHex(cols[0].Value) + DecToHex(cols[1].Value) + DecToHex(cols[2].Value) + DecToHex(a.ToString());
			}
			return result;
		}
		public static string DecToHex(string num)
		{
			string hex = String.Format("{0:X}", int.Parse(num));
			if(hex.Length == 1)
			{
				hex = "0" + hex;
			}
			return hex;
		}
	}
}