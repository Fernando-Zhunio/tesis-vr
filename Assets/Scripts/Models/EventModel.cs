

[System.Serializable]
public class EventModel
{
    public int id;
    public string name;
    public string description;
    public string image;
    public double[] position;
    public int status;
    public string start_date;
    public string end_date;
    public string created_at;
    public string updated_at;

    public bool is_favorite;
    public bool has_liked;

    public bool has_favorited;
}
