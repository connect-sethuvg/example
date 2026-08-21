using Example.Data.Contracts;
using ExampleMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMS.Data.Entities
{
    public class ExampleData : BaseEntity, IExample
    {
        public string Name { get ; set; }
        public string Description { get ; set ; }
        public int ActiveStatus { get; set; }
        public long? CreatedUserId { get; set; }
        public long? EditedUserId { get; set; }
    }
}
