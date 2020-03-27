using BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BlazorApp.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static string PrintAllProperties(this object obj)
        {
            var result = "";
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name = descriptor.Name;
                object type = descriptor.GetType();
                result += $"{name} {type}";
            }

            return result.ToString();
        }


    }
}
