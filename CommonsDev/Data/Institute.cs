using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Commons.Identity;
using CommonsDev.Models;

namespace CommonsDev.Data
{
    public class Institute : IWorkSpace
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }

        [NotMapped]
        public TestClass Test { get; set; } = new TestClass(){Name = $"testName"};

        [NotMapped]
        public bool ThisIsBool { get; set; }

        public int GimmeFive()
        {
            return 5;
        }

        public int GimmeFives => GimmeFive();
    }
}
