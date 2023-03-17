using System;
using System.Collections.Generic;
using System.Text;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel.Attribute
{
    /// <summary>
    /// Config Class Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConfigClassAttribute:System.Attribute
    {
        private string SectionName { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sectionName"></param>
        public ConfigClassAttribute(string sectionName)
        {
            SectionName = sectionName;
        }
    }
}
