using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMS.Framework.Data.Entities
{
    public interface IAudiable
    {
        #region Member variable
        long? CreatedUserId { get; set; }
        long? EditedUserId { get; set; }
        #endregion
    }
}
