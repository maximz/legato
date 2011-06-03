using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace FindPianos.ViewModels
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
        /// Gets or sets the types that the instrument in question can be; e.g., acoustic or electric.
        /// </summary>
        /// <value>The types.</value>
        [DisplayName("Possible instrument types")]
        public IEnumerable<SelectListItem> Types
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the selected instrument style.
        /// </summary>
        /// <value>The selected instrument style.</value>
        [Required(ErrorMessage="You must select an instrument style.")]
        [DisplayName("Selected instrument type")]
        public int SelectedStyle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets possible instrument styles; e.g., grand, upright, or baby grand.
        /// </summary>
        /// <value>The styles.</value>
        [DisplayName("Possible instrument types")]
        public IEnumerable<SelectListItem> Styles
        {
            get;
            set;
        }
    }
}