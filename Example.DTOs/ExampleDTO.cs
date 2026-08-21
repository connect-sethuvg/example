using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Example.DTOs
{
    public class ExampleDTO
    {
        #region Member
        public long Id {  get; set; }
        public string Name { get; set; }
        public int ActiveStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        #endregion
    }
}
