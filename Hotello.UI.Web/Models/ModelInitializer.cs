using System.Collections.Generic;
using System.Linq;

namespace Hotello.UI.Web.Models
{
    public static class ModelInitializer
    {
        public static SearchViewModel CreateSearchModel()
        {
            return new SearchViewModel
                {
                    NumberOfBedrooms = 1, // Default to 1 bedroom?
                    RoomViewModels = new List<RoomViewModel>(Enumerable.Range(1, 4) // From 1 to 4 rooms
                        .Select((room, idx) => new RoomViewModel()
                            {
                                Adults = idx == 1 ? 1 : 0,
                                Children = null,
                                AgeViewModels = new List<AgeViewModel>(Enumerable.Range(1, 3)
                                    .Select(i => new AgeViewModel()
                                        {
                                            Age = null,
                                        }))
                            })),
                };
        }
    }
}