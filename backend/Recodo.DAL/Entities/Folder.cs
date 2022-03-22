using System.Collections.Generic;

namespace Recodo.DAL.Entities
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
