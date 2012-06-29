using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Legato.Models.Email
{
    public class EmailManager
    {
        /// <summary>
        /// Gets the current <see cref="EmailManager"/> for the application.
        /// </summary>
        public static EmailManager Current
        {
            get
            {
                return Nested.Current;
            }
        }

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        class Nested
        {
            internal static readonly EmailManager Current = new EmailManager();

            static Nested()
            {
            }
        }


    }
}