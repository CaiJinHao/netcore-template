using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Extension
{
    public static class ObjectIdExtension
    {
        public static bool IsNull(this ObjectId pObjectId)
        {
            if (pObjectId.Increment>0)
            {
                return true;
            }
            return false;
        }
    }
}
