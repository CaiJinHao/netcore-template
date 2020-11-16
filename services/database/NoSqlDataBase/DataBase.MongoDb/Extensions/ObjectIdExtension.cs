using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBse.Extensions
{
    public static class ObjectIdExtension
    {
        public static bool IsNull(this ObjectId pObjectId)
        {
            if (pObjectId.Timestamp > 0)
            {
                return true;
            }
            return false;
        }
    }
}
