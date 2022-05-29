public class StackableItemTemplate : ItemTemplate {
    public int MaxQuantity;

    public StackableItemTemplate(int id, string name, string formattedName, string title, ItemSize size, int maxQuantity) : base(id, name, formattedName, title, size) {
        this.Id = id;
        this.Name = name;
        this.Size = size;
        this.MaxQuantity = maxQuantity;
    }
}