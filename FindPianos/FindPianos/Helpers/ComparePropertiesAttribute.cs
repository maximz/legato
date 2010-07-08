using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FindPianos.Helpers
{
    //from: http://stackoverflow.com/questions/1607832/writing-a-compareto-dataannotation-attribute
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ComparePropertiesAttribute : ValidationAttribute
    {
        public string ComparisonProperty1
        {
            get;
            set;
        }
        public string ComparisonProperty2
        {
            get;
            set;
        }
        public bool LessThanAllowed
        {
            get;
            set;
        }
        public bool EqualToAllowed
        {
            get;
            set;
        }
        public bool GreaterThanAllowed
        {
            get;
            set;
        }
        public bool AllowNullValues
        {
            get;
            set;
        }

        private static IComparable GetComparablePropertyValue(object obj, string propertyName)
        {
            if (obj == null) return null;
            var type = obj.GetType();
            var propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null) return null;
            return propertyInfo.GetValue(obj, null) as IComparable;
        }

        public override bool IsValid(object value)
        {
            var comp1 = GetComparablePropertyValue(value, ComparisonProperty1);
            var comp2 = GetComparablePropertyValue(value, ComparisonProperty2);

            if (comp1 == null && comp2 == null)
            {
                return AllowNullValues;
            }
            if (comp1 == null || comp2 == null)
                return false;

            var result = comp1.CompareTo(comp2);

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