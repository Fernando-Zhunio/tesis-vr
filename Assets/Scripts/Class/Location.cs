[System.Serializable]
public class Location {
    public string latitud;
    public string longitud;
    public string altitude;

    public Location(double latitud, double longitud, double altitude = 0)
    {
        this.latitud = latitud.ToString().Replace(",", ".");
        this.longitud = longitud.ToString().Replace(",",".");
        this.altitude = altitude.ToString().Replace(",",".");
    }

    public string GetLatLonString()
    {
        return this.latitud + "," + this.longitud;
    }

    public string GetLatLonAltString(string separator)
    {
        return this.latitud + "," + this.longitud + "," + this.altitude;
    }

}