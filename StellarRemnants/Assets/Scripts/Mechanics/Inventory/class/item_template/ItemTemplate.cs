using UnityEngine.Localization.SmartFormat;

public class ItemTemplate {
    public int Id;
    public string Name;
    public string FormattedName;
    public ItemSize Size;
    public string Title;
    
    public ItemTemplate(int id, string name, string formattedName, string title, ItemSize size) {
        this.Id = id;
        this.Name = name;
        this.FormattedName = formattedName;
        this.Size = size;
        this.Title = title;
    }

    public string GetFormattedName(int count) {
        return Smart.Format(FormattedName, count);
    }
}