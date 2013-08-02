using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeFiles.Abstracts
{
    
    public class Factory
    {
        ORM orm;
        SQLLite sqlLight;
        public Factory()
        {
            orm = new ORM();
            sqlLight = new SQLLite();
        }
        private static Factory instance;
        public static Factory Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new Factory();
                }
                return instance;
            }
        }
        public ORM ProvideORM()
        {
            return orm;
        }

        public SQLLite ProvideSQLLight()
        {
            return sqlLight;
        }
    }
}