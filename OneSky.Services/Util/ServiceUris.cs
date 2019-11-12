namespace OneSky.Services.Util{
    public class ServiceUris{
// Routing Services
        public const string PointToPointRouteUri = "/V1/vehiclePath/pointToPoint";
        public const string Sgp4RouteUri = "/V1/vehiclePath/sgp4";
        public const string SimpleFlightRouteUri = "/V1/vehiclePath/simpleFlight";
        public const string GreatArcRouteUri = "/V1/vehiclePath/greatArc";
        public const string TolRouteUri = "/V1/vehiclePath/tol";
        public const string RasterRouteUri = "/V1/vehiclePath/raster";
        public const string CatalogObjectRouteUri = "/V1/vehiclePath/catalogObject";
//Satellite Pass Services
        public const string AccessSatellitePassesSiteUri = "/V1/access/satellitePasses/site";
        public const string AccessSatellitePassesSgp4Uri = "/V1/access/satellitePasses/sgp4";
        public const string AccessSatellitePassesPointToPointUri = "/V1/access/satellitePasses/pointToPoint";
        public const string AccessSatellitePassesCatalogObjectUri = "/V1/access/satellitePasses/catalogObject";
        public const string AccessSatellitePassesRasterUri = "/V1/access/satellitePasses/raster";
        public const string AccessSatellitePassesTolUri = "/V1/access/satellitePasses/tol";
        public const string AccessSatellitePassesSimpleFlightUri = "/V1/access/satellitePasses/simpleFlight";
        public const string AccessSatellitePassesGreatArcUri = "/V1/access/satellitePasses/greatArc";
        //TODO add SensorFOR URLs here
// Communication Services
        public const string CommunicationsLinkBudgetUri = "/V1/communications/linkbudget";
// Navigation Services
        public const string NavigationGpsOutagesUri = "/V1/navigation/gpsSatelliteOutages";
        public const string NavigationPafDataUri = "/V1/navigation/PAFData";
        public const string NavigationPsfDataUri = "/V1/navigation/PSFData";
        public const string NavigationSofDataUri = "/V1/navigation/SOFData";
        public const string NavigationAlmanacDataUri = "/V1/navigation/AlmanacData";
        public const string NavigationPredictedSiteUri = "/V1/navigation/predicted/site";
        public const string NavigationPredictedPointToPointUri = "/V1/navigation/predicted/pointToPoint";
        public const string NavigationPredictedTolUri = "/V1/navigation/predicted/tol";
        public const string NavigationPredictedRasterUri = "/V1/navigation/predicted/raster";
        public const string NavigationPredictedGreatArcUri = "/V1/navigation/predicted/greatArc";
        public const string NavigationPredictedSimpleFlightUri = "/V1/navigation/predicted/simpleFlight";
        public const string NavigationAssessedSiteUri = "/V1/navigation/assessed/site";
        public const string NavigationAssessedPointToPointUri = "/V1/navigation/assessed/pointToPoint";
        public const string NavigationAssessedTolUri = "/V1/navigation/assessed/tol";
        public const string NavigationAssessedRasterUri = "/V1/navigation/assessed/raster";
        public const string NavigationAssessedGreatArcUri = "/V1/navigation/assessed/greatArc";
        public const string NavigationAssessedSimpleFlightUri = "/V1/navigation/assessed/simpleFlight";
        public const string NavigationDopSiteUri = "/V1/navigation/dop/site";
        public const string NavigationDopPointToPointUri = "/V1/navigation/dop/pointToPoint";
        public const string NavigationDopTolUri = "/V1/navigation/dop/tol";
        public const string NavigationDopRasterUri = "/V1/navigation/dop/raster";
        public const string NavigationDopGreatArcUri = "/V1/navigation/dop/greatArc";
        public const string NavigationDopSimpleFlightUri = "/V1/navigation/dop/simpleFlight";
// Ovrflight Services
        public const string OverflightSgp4Uri = "/V1/overflight/sgp4";
        public const string OverflightSimpleFlightUri = "/V1/overflight/simpleFlight";
        public const string OverflightPointToPointUri = "/V1/overflight/pointToPoint";
        public const string OverflightGreatArcUri = "/V1/overflight/greatArc";
        public const string OverflightCatalogObjectUri = "/V1/overflight/catalogObject";
// Airspace Services
        public const string AirspaceSimpleFlightUri = "/V1/airspace/simpleFlight";
        public const string AirspacePointToPointUri = "/V1/airspace/pointToPoint";
        public const string AirspaceGreatArcUri = "/V1/airspace/greatArc";
        public const string AirspacePointFlightUri = "/V1/airspace/pointFlight";
        public const string AirspaceSelectAirspacesUri = "/V1/airspace/selectAirspaces";
        public const string AirspaceRealTimeUri = "/V1/airspace/realtime";
// CZML visualization
        public const string VehiclePathCzmlSgp4Uri = "/V1/vehiclepath/czml/sgp4";
        public const string VehiclePathCzmlGpsUri = "/V1/vehiclepath/czml/Gps";
        public const string GlobalPredictedGpsCzmlUri = "/V1/navigation/predicted/czml/global";
        public const string AirspaceCzmlUri = "/V1/airspace/czml";
// Terrain Services
        public const string TerrainHeightsSiteUri = "/V1/terrain/heights";
        public const string TerrainHeightsPointToPointUri = "/V1/terrain/pointToPoint";    
        public const string TerrainHeightsGreatArcUri = "/V1/terrain/greatArc";
// Lighting Services
        public const string LightingSiteUri = "/V1/lighting/site";
        public const string LightingPointToPointUri = "/V1/lighting/pointToPoint";
        public const string LightingAnglesSiteUri = "/V1/lighting/angles/site";
        public const string LightingAnglesPointToPointUri = "/V1/lighting/angles/pointToPoint";
        public const string LightingSolarTransitSiteUri = "/V1/lighting/transit/site";
// Population Services
        public const string PopulationPointToPointUri = "/V1/population/pointToPoint";
        public const string PopulationSiteUri = "/V1/population/site";
// Weather Services
        public const string WeatherPointToPointUri = "/V1/weather/pointToPoint";
        public const string WeatherSiteUri = "/V1/weather/site";

    }
}