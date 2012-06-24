using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using Legato.Helpers;

namespace Legato.ViewModels
{
    public class EquipmentViewModel
    {
        /// <summary>
        /// Gets or sets the brand of the instrument.
        /// </summary>
        /// <value>The brand.</value>
        //[Required(ErrorMessage="Brand name is required.")]
        [DisplayName("Brand")]
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
        [DisplayName("Model")]
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
        [DisplayName("Type")]
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
        [DisplayName("Availability")]
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
        /// Gets or sets the selected address privacy setting.
        /// </summary>
        /// <value>The selected address privacy setting.</value>
        [Required(ErrorMessage = "You must select a privacy setting.")]
        [DisplayName("Address privacy")]
        public int SelectedPrivacy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets possible address privacy settings; e.g., full address, neighborhood (zip code) only, city only.
        /// </summary>
        /// <value>The styles.</value>
        [DisplayName("Possible address privacy settings")]
        public IEnumerable<SelectListItem> PrivacySettings
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
                Classes = new SelectList(InstrumentClasses.Classes, InstrumentClasses.ValueField, InstrumentClasses.TextField);
            }

            if (Types == null)
            {
                /*var dbTypes = (from t in Current.DB.InstrumentTypes
                               orderby t.TypeID ascending
                               select new { Id = t.TypeID, Name = t.Name }).ToArray();*/
                // uncomment above and comment below for alphabetical listing of types. instead, piano first:

                var pianoType = Current.DB.InstrumentTypes.Where(t => t.Name.Contains("Piano")).Select(t=> new { Id = t.TypeID, Name = t.Name }).ToArray();
                var otherTypes = (from t in Current.DB.InstrumentTypes
                                  orderby t.TypeID ascending
                                  where t.Name.Contains("Piano") == false
                                  select new { Id = t.TypeID, Name = t.Name }).ToArray();
                var dbTypes = pianoType.Concat(otherTypes).ToArray();
                Types = new SelectList(dbTypes, "Id", "Name");
            }

            if (PrivacySettings == null)
            {
                PrivacySettings = new SelectList(AddressPrivacySettings.Settings, AddressPrivacySettings.ValueField, AddressPrivacySettings.TextField);
            }
        }
    }
}