using ExampleMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Example.Data.Contracts
{
    public interface IExample : IAudiable, IEntity
    {
        public string Surame { get; set; }
        public string Description { get; set; }
        public int ActiveStatus { get; set; }
    }
}
