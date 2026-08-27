using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMS.Framework.Data.Entities
{
    public abstract class BaseEntity : IEntity
    {
        #region Member
        public  long Id { get; set; }
        public  DateTime? CreatedDate { get; set; }
        public  DateTime? EditedDate { get; set; }
        #endregion
    }
}
