using System;

namespace playground.Assets.Scripts.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute()
        {

        }
    }
}