namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class Category
    {
        public byte Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Mod> Mods { get; set; }
    }
}
