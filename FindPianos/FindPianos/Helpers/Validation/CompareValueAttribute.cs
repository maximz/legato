using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.Helpers
{
    //from: http://stackoverflow.com/questions/1607832/writing-a-compareto-dataannotation-attribute
    /// <summary>
    /// Compares a property to a given comparison value. Applied to a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CompareValueAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the comparison value.
        /// </summary>
        /// <value>The comparison value.</value>
        public object ComparisonValue
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the first property can be less than the second property.
        /// </summary>
        /// <value><c>true</c> if less than is allowed; otherwise, <c>false</c>.</value>
        public bool LessThanAllowed
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the first property can be equal to the second property.
        /// </summary>
        /// <value><c>true</c> if equal to is allowed; otherwise, <c>false</c>.</value>
        public bool EqualToAllowed
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the first property can be greater than the second property.
        /// </summary>
        /// <value><c>true</c> if greater than is allowed; otherwise, <c>false</c>.</value>
        public bool GreaterThanAllowed
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether null values are allowed.
        /// </summary>
        /// <value><c>true</c> if null values are allowed; otherwise, <c>false</c>.</value>
        public bool AllowNullValues
        {
            get;
            set;
        }
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return AllowNullValues;
            }
            if(ComparisonValue==null)
            {
                throw new ArgumentNullException("ComparisonValue");
            }

            int result = 0;

            if(ComparisonValue as string == "DateTime.Now")
            {
                result = (value as IComparable).CompareTo((DateTime.Now as IComparable));
            }
            else
            {
                result = (value as IComparable).CompareTo((ComparisonValue as IComparable));
            }

            

            switch (result)
            {
                case -1: return LessThanAllowed;
                case 0: return EqualToAllowed;
                case 1: return GreaterThanAllowed;
                default: throw new ApplicationException("Something just failed in ComparePropertiesAttribute");
            }
        }
    }
}