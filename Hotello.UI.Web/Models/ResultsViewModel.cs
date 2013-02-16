using System.Collections.Generic;
using System.Web.Mvc;

namespace Hotello.UI.Web.Models
{
    public class ResultsViewModel
    {
        public static SelectList ResultsOrderList
        {
            get
            {
                return new SelectList(new List<SelectListItem>()
                    {
                        new SelectListItem()
                            {
                                Selected = false,
                                Text = "Price - Low to High",
                                Value = "Price asce"
                            },
                        new SelectListItem()
                            {
                                Selected = false,
                                Text = "Price - High to Low",
                                Value = "Price desc"
                            },
                        new SelectListItem()
                            {
                                Selected = false,
                                Text = "Rating - Low to High",
                                Value = "Rating asce"
                            },

                        new SelectListItem()
                            {
                                Selected = false,
                                Text = "Rating - High to Low",
                                Value = "Rating desc"
                            },
                    }, "Value", "Text");
            }
        }
    }
}