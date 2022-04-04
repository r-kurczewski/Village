using BayatGames.SaveGameFree.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village
{
	/// <summary>
	/// Skips stronger encoding and leave only Base64 encoding
	/// </summary>
	public class PlainEncoder : ISaveGameEncoder
	{
		public string Decode(string input, string password)
		{
			return input;
		}

		public string Encode(string input, string password)
		{
			return input;
		}
	}
}
