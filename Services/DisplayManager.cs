using EC.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services
{
	public class DisplayManager : IService
	{

		private int width;
		private int height;

		public int Width
		{
			get => width;
			set
			{
				width = value;
				UpdateWindowCenter();
			}
		}

		public int Height
		{
			get => height;
			set
			{
				height = value;
				UpdateWindowCenter();
			}
		}

		public Vector2 WindowCenter { get; private set; }

		private void UpdateWindowCenter()
		{
			WindowCenter = new Vector2(width/2, height/2);
		}

	


	}
}
