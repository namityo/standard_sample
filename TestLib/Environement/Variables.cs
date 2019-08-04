using System;
using System.Collections.Generic;
using System.Text;

namespace TestLib.Environement
{
    /// <summary>
    /// このクラスは適当に作った変数受け渡しクラス
    /// </summary>
    public class Variables
    {
        private static Variables _instance = null;

        public static Variables GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Variables();
            }

            return _instance;
        }

        private Variables()
        {
        }

        public string DataStoreConnectionString { get; set; }
    }
}
