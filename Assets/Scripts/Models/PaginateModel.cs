using System.Collections.Generic;

[System.Serializable]
public class PaginateModel<T>
{
    
    public T[] data;
    public int current_page;
    public string first_page_url;
    public int from;
    public int last_page;
    public string last_page_url;
    public Link[] links;
    public string next_page_url;
    public string path;
    public int per_page;
    public object prev_page_url;
    public int to;
    public int total;
}

[System.Serializable]
public class Link
{
    public string url;
    public string label;
    public bool active;
}
