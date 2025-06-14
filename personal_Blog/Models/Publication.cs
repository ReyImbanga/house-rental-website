namespace personal_Blog.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } // HTML content from CKEditor
        public string? ProfileImagePath { get; set; } //une seule image
        public string? ImagePaths { get; set; } //liste d'images supplémentaires (JSON or comma-separated list)
        public string? TypeHouse { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
