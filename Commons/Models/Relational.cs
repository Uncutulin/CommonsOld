using System;

namespace Commons.Models
{
    public abstract class Relational
    {
        public string Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }




        public bool IsActive()
        {
            return DeletedDate == null;
        }

        public void Delete()
        {
            DeletedDate = DateTime.Now;
        }
    }
}
