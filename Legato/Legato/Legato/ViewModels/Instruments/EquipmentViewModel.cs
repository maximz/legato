using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace Legato.ViewModels
{
    public class EquipmentViewModel
    {
        /// <summary>
        /// Gets or sets the brand of the instrument.
        /// </summary>
        /// <value>The brand.</value>
        [Required(ErrorMessage="Brand name is required.")]
        [DisplayName("Instrument brand")]
        [StringLength(50,ErrorMessage="The brand name is too long. It must be under 50 characters in length.")]
        public string Brand
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the model of the instrument.
        /// </summary>
        /// <value>The model.</value>
        [DisplayName("Instrument model")]
        [StringLength(50, ErrorMessage = "The model name is too long. It must be under 50 characters in length.")]
        public string Model
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the selected instrument type.
        /// </summary>
        /// <value>The selected instrument type.</value>
        [Required(ErrorMessage="You must select a type.")]
        [DisplayName("Selected instrument type")]
        public int SelectedType
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the types that the instrument in question can be; e.g., piano, clarinet, saxophone.
        /// </summary>
        /// <value>The types.</value>
        [DisplayName("Possible instrument types")]
        public IEnumerable<SelectListItem> Types
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the selected instrument class.
        /// </summary>
        /// <value>The selected instrument class.</value>
        [Required(ErrorMessage="You must select an instrument class.")]
        [DisplayName("Selected instrument class")]
        public int SelectedClass
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets possible instrument styles; e.g., public, for sale, for rent.
        /// </summary>
        /// <value>The styles.</value>
        [DisplayName("Possible instrument classes")]
        public IEnumerable<SelectListItem> Classes
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentViewModel"/> class.
        /// </summary>
        public EquipmentViewModel()
        {
            if (Classes == null)
            {
                    Classes = new SelectList(new[]
                {
                    new { Id = 1, Name = "Public" },
                    new { Id = 2, Name = "Rent" },
                    new { Id = 3, Name = "Sale" },
                }, "Id", "Name");
            }

            if (Types == null)
            {
                var dbTypes = (from t in Current.DB.InstrumentTypes
                               select new { Id = t.TypeID, Name = t.Name }).ToArray();
                Types = new SelectList(dbTypes, "Id", "Name");
            }
        }
    }
}