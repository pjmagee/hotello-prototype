using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Hotello.Common;
using Hotello.Services.Expedia.Hotels.Models;

namespace Hotello.UI.Web.Models
{
    public class SearchViewModel
    {
        // Required for Hotel availability

        [Required]
        [Display(Name = "Check-in")]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [Display(Name = "Check-out")]
        public DateTime DepartureDate { get; set; }

        [Required]
        public int NumberOfBedrooms { get; set; }

        public List<RoomViewModel> RoomViewModels { get; set; }

        // Destination Search Method 1
        [Required]
        public string Destination { get; set; }


        public string DestinationId { get; set; }

        // Destination Search Method 2 
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        // Filtering Search Methods
        public float? MinimumStarRating { get; set; }
        public float? MaximumStarRating { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }

        public List<int> PropertyCategories { get; set; }
        public List<int> Amenities { get; set; }
        
        // Optional requirements
        public int ResultsPerPage { get; set; }
        public int Option { get; set; }

        public MultiSelectList AmenitiesMultiList
        {
            get
            {
                return new MultiSelectList(Enum.GetValues(typeof(Amenity)).Cast<Amenity>()
                    .Select(amenity => new SelectListItem()
                    {
                        Selected = false,
                        Value = ((int)amenity).ToString(),
                        Text = amenity.GetDescription()
                    }), 
                    "Value", "Text");
            }
        }

        public MultiSelectList CategoriesMultiList
        {
            get
            {
                return new MultiSelectList(Enum.GetValues(typeof (PropertyCategory)).Cast<PropertyCategory>()
                    .Select(propertyCategory => new SelectListItem()
                        {
                            Selected = false,
                            Value = ((int) propertyCategory).ToString(),
                            Text = propertyCategory.GetDescription()
                        }), "Value", "Text");
            }
        }

        public SelectList StarRatingList
        {
            get
            {
                return new SelectList(new List<float>() { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 4.5f, 5f }.Select(rating => 
                    new SelectListItem()
                        {
                            Selected = false, 
                            Text = Convert.ToString(rating), 
                            Value = Convert.ToString(rating)
                        }), "Value", "Text");
            }
        }

        public static SelectList RoomsSelectList
        {
            get
            {
                // 1 to 4
                return new SelectList(Enumerable.Range(1, 4).Select(i => 
                    new SelectListItem()
                    {
                        Selected = i == 1,
                        Text = Convert.ToString(i),
                        Value = Convert.ToString(i),
                    }), "Value", "Text");
            }
        }

        public static SelectList AdultSelectList
        {
            get
            {
                return new SelectList(Enumerable.Range(1, 4).Select(i =>
                       new SelectListItem
                       {
                           Selected = i == 1,
                           Text = Convert.ToString(i),
                           Value = Convert.ToString(i),
                       }), "Value", "Text");
            }
        }

        public static SelectList ChildrenSelectList
        {
            get
            {
                // Children from 1 to 3
                return new SelectList(Enumerable.Range(1, 3).Select(i =>
                    new SelectListItem()
                    {
                        Selected = false,
                        Text = Convert.ToString(i),
                        Value = Convert.ToString(i),
                    }), "Value", "Text");
            }
        }

        public static SelectList ChildrenAgesSelectList
        {
            get
            {
                // Ages 1 to 17
                return new SelectList(Enumerable.Range(1, 17).Select(i =>
                    new SelectListItem()
                    {
                        Selected = false,
                        Text = Convert.ToString(i),
                        Value = Convert.ToString(i),
                    }), "Value", "Text");
            }
        }

        public SelectList CategoryList
        {
            get
            {
                return new SelectList(Enum.GetValues(typeof(PropertyCategory))
                    .Cast<PropertyCategory>().Select(category =>
                        new SelectListItem()
                            {
                                Selected = false,
                                Text = category.GetDescription(),
                                Value = ((int)category).ToString()
                            }),
                            "Value","Text");
            }
        }

        public SelectList ResultsPerPageList
        {
            get
            {
                return new SelectList(new List<int>() { 5, 10, 20, 30, 40, 50, 60 }.Select(result =>
                    new SelectListItem()
                        {
                            Selected = false, 
                            Text = result.ToString(), 
                            Value = result.ToString()
                        }
                    ),
                    "Value", "Text");
            }
        }

        public SelectList OptionsList
        {
            get
            {
                return new SelectList(Enum.GetValues(typeof(Options)).Cast<Options>().Select(option => 
                    new SelectListItem
                    {
                        Selected = false,
                        Text = option.GetDescription(),
                        Value = ((int)option).ToString()
                    }), 
                    "Value", "Text");
            }

        }

    }

    public class AgeViewModel
    {
        public int? Age { get; set; }
    }

    public class RoomViewModel
    {
        public int? Adults { get; set; }
        public int? Children { get; set; }

        public List<AgeViewModel> AgeViewModels { get; set; }
    }
    
    public class AmenityCheckBox
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Checked { get; set; }
    }

}