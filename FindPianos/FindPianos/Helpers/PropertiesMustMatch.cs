using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FindPianos.Helpers
{
    //from: http://byatool.com/mvc/custom-data-annotations-with-mvc-how-to-check-multiple-properties-at-one-time/
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=true)]
    public class PropertiesMustMatchAttribute : ValidationAttribute
    { 
        public String FirstPropertyName { get; set; }
        public String SecondPropertyName { get; set; } 

        //Constructor to take in the property names that are supposed to be checked
        public PropertiesMustMatchAttribute(String firstPropertyName, String secondPropertyName )
        {
            FirstPropertyName = firstPropertyName;
            SecondPropertyName = secondPropertyName ;
        } 

        public override Boolean IsValid(Object value)
        {
            Type objectType = value.GetType();
            //Get the property info for the object passed in.  This is the class the attribute is
            //  attached to
            //I would suggest caching this part... at least the PropertyInfo[]
            PropertyInfo[] neededProperties =
            objectType.GetProperties()
            .Where(propertyInfo => propertyInfo.Name == FirstPropertyName || propertyInfo.Name == SecondPropertyName)
            .ToArray();

            if(neededProperties.Count() != 2)
            {
            throw new ApplicationException("PropertiesMustMatchAttribute error on " + objectType.Name);
            }

            Boolean isValid = true;

            //Convert both values to string and compare...  Probably could be done better than this
            //  but let's not get bogged down with how dumb I am.  We should be concerned about
            //  dumb you are, jerkface.
            if(!Convert.ToString(neededProperties[0].GetValue(value, null)).Equals(Convert.ToString(neededProperties[1].GetValue(value, null))))
            {
                isValid = false;
            }

            return isValid;
        }
    }

    //todo separate these attributes into separate classes under one helpers/validation folder
    public class MinimumValueAttribute : RangeAttribute
    {
        public MinimumValueAttribute(double minimum) : base(minimum, double.MaxValue)
        {
        }
    }

    public class MaximumValueAttribute : RangeAttribute
    {
        public MaximumValueAttribute(double maximum)
            : base(double.MinValue, maximum)
        {
        }
    }

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
