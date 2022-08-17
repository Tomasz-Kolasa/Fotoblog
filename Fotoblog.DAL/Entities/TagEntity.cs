﻿namespace Fotoblog.DAL.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PhotoEntity> ?Photos { get; set; }
    }
}
