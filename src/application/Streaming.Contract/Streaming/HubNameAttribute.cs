using System;

namespace Super.Paula.Application.Streaming
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class HubNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public HubNameAttribute(string name)
        {
            Name = name;
        }
    }
}
