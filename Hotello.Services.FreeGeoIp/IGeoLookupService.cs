namespace Hotello.Services.GeoIp
{
    public interface IGeoLookupService
    {
        GeoLookUpResponse GetGeoFromIp(GeoLookUpRequest request);
    }
}