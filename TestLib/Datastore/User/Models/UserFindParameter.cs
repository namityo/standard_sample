using System;
using System.Collections.Generic;
using System.Text;

namespace TestLib.Datastore.User.Models
{
    public class UserFindParameter
    {
        // 年齢の下限
        public int? AgeLower { get; set; }
        // 年齢の上限
        public int? AgeUpper { get; set; }
    }
}
