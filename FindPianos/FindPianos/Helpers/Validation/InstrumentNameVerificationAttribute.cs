using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FindPianos.Models;

namespace FindPianos.Helpers
{
    /// <summary>
    /// Verifies that the given instrument name exists as an instrument within our database.
    /// </summary>
    public class InstrumentNameVerificationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentNameVerificationAttribute"/> class.
        /// </summary>
        public InstrumentNameVerificationAttribute() : base()
        {
            ErrorMessage = "That instrument doesn't exist in our system.";
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
            if (value == null) //we don't care if it's required or not.
            {
                return true;
            }
            var address = (string)value;
            try
            {
                using (var db = new LegatoDataContext())
                {
                    db.Instruments.Where(i => i.Name == value.ToString()).Single();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}