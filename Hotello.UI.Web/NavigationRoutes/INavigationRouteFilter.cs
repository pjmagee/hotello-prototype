using System.Web.Routing;

namespace Hotello.UI.Web.NavigationRoutes
{
    public interface INavigationRouteFilter
    {
        bool  ShouldRemove(Route navigationRoutes);
    }
}
