using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Developers
{
    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hobby { get; set; }
        public DateTime BirthDate { get; set; }
        public Sex Sex { get; set; }

        public virtual int Age { get => DateTime.Today.Year - BirthDate.Year;}
    }
}
