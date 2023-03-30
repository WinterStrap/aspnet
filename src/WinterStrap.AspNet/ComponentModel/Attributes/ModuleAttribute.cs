using System;
using System.Collections.Generic;
using System.Text;

namespace WinterStrap.AspNet.ComponentModel.Attributes;

/// <summary>
/// Represent a WinterStrap module.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ModuleAttribute : Attribute
{
}
