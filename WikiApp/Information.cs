using Microsoft.VisualBasic;
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
    /// Use private properties for the fields which must be of type “string”
    /// The class file must have separate setters and getters
    /// Add an appropriate IComparable for Name attribute
    /// Save the class as “Information.cs”
    /// </summary>
    /// <remarks>
    /// icomparable <see href="https://learn.microsoft.com/en-us/dotnet/api/system.icomparable?view=net-7.0"/>
    /// </remarks>
    //: IComparable<T> instead of : IComparable allows instances to be compared to only <T> type objects
    internal class Information : IComparable<Information>
    {
        //Fields with private access modifiers
        private string _name;
        private string _category;
        private string _structure;
        private string _definition;
        //Primary default/empty constructor (compiler generates default parameterless constructor, initialised with null values)
        public Information()
        {
            //Can replace with =null; =String.Empty or "";
            _name = "~";
            _category = "~";
            _structure = "~";
            _definition = "~";
        }

        //Secondary overloaded constructor
        public Information(string name, string category, string structure, string definition)
        {
            _name = name;
            _category = category;
            _structure = structure;
            _definition = definition;
        }

        //Properties: Getters and setters 
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string Structure
        {
            get { return _structure; }
            set { _structure = value; }
        }

        public string Definition
        {
            get { return _definition; }
            set { _definition = value; }
        }
        //Implementation of IComparable<T> interface for member field/variable name
        public int CompareTo(Information other)
        {
            //-1 if a is less than b
            //0 if a is equal to b
            //1 if a is greater than b
            if (other == null) return 1;
            return this._name.CompareTo(other._name);
        }
    }
    #endregion
}
/* Notes
 * ListClass (2020, May 5). Personal communication [Visual Studio Solution]. https://blackboard.southmetrotafe.wa.edu.au
 * Private instance fields start with an underscore (_). https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names
        //property Quickaction and Refractoring...
        // => used in property is an expression body introduced from C#6, a shorthand for return
        public string Name { get => _name; set => _name = value; }
        public string Category { get => _category; set => _category = value; }
        public string Structure { get => _structure; set => _structure = value; }
        public string Definition { get => _definition; set => _definition = value; }
 */