using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.Colliders
{
	public interface ICollider
	{
		bool Contains(Vector2 point);
	}
}
