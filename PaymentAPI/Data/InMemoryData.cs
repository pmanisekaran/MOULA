using Microsoft.CodeAnalysis.CSharp.Syntax;
using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Data
{
	public static class InMemoryData
	{
		static InMemoryData()
		{
			InMemoryData.CurrentBalance = 10000;
		}

		public static Decimal CurrentBalance { get; set; }

		public static List<Payment> Payments { get; } = new List<Payment>();
	}
}
