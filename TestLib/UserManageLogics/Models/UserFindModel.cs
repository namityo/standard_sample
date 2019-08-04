using System;
using System.Collections.Generic;
using System.Text;

namespace TestLib.UserManageLogics.Models
{
    public class UserFindModel
    {
        /* 検索条件で使う変数を設定する */
        public int? AgeLower { get; set; }

        public int? AgeUpper { get; set; }
    }
}
