using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMS.Framework.Data.Entities
{
    public interface IEntity
    {
        #region Member 
        long Id { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? EditedDate { get; set; }
        #endregion
    }
}
