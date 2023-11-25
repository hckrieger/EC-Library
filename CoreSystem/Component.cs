using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.CoreSystem
{
    public class Component
    {
        public Entity Entity { get; set; }

        public bool IsEnabled { get; set; }

        public Component(Entity entity)
        {
            Entity = entity; 
            IsEnabled = true;
        }

        public virtual void Intitialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}
