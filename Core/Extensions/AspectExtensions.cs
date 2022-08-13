﻿using System.Reflection;

namespace Core.Extensions
{
    public static class AspectExtensions
    {
        public static bool CheckPasswordProperty(this object obja)
        {
            PropertyInfo[] properties = obja.GetType().GetProperties();
            if (properties.FirstOrDefault(p => p.Name.ToLower().Contains("password")) != null)
            {
                return true;
            }
            return false;
        }
    }
}