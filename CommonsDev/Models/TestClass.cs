using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Commons.Extensions.Attributes;
using Commons.Models;
using CommonsDev.Data;

namespace CommonsDev.Models
{
    public class TestClass : Documental
    {
        [Unique]
        public string Name { get; set; }

        [Unique(groupId: "Doc", 0)]
        public string TipoDoc { get; set; }

        [Unique(groupId: "Doc", 1)]
        public string Doc { get; set; }
    }
}
