using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiApp
{
    #region INFORMATION class
    /// <summary>
    /// 6.1 Create a separate class file to hold the four data items of the Data Structure (use the Data Structure Matrix as a guide).
    /// Use private properties for the fields which must be of type “string”.
    /// The class file must have separate setters and getters
    /// Add an appropriate IComparable for the Name attribute.
    /// Save the class as “Information.cs”. 
    /// </summary>
    internal class Information
    {
        private string _name;
        private string _category;
        private string _structure;
        private string _definition;
        //property
        // => used in property is an expression body introduced from C#6, a shorthand for return
        public string Name { get => _name; set => _name = value; }
        public string Category { get => _category; set => _category = value; }
        public string Structure { get => _structure; set => _structure = value; }
        public string Definition { get => _definition; set => _definition = value; }
    }
    #endregion
}
