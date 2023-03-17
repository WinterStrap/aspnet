using System;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel.Attribute
{
    /// <summary>
    /// Config Class Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConfigurationAttribute : System.Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sectionName"></param>
        public ConfigurationAttribute(string sectionName)
        {
            _sectionName = sectionName;
        }

        private readonly string _sectionName;
        /// <summary>
        /// Gets the section name
        /// </summary>
        public string SectionName => _sectionName;
    }
}
