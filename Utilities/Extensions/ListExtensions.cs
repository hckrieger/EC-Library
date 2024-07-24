﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Utilities.Extensions
{
	public static class ListExtensions
	{


		public static void Shuffle<T>(this List<T> list)
		{
			int n = list.Count;
			for (int i = n - 1; i >= 0; i--) 
			{
				int j = MathUtils.RandomInt(0, i + 1);
				T temp = list[i];
				list[i] = list[j];
				list[j] = temp;
			}
		}
	}
}
