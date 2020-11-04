using System;

namespace Engine
{
    public interface ISystem
    {
        public void Simulate(Single timestep);

        public Boolean RegisterComponent(Entity entity);

        public Component GetComponent(Entity entity);
    }
}
