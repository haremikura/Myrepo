using MVCFramework.Repositries;
using MVCFramework.Models.Entity;
using System;
using System.Collections.Generic;
using MVCFramework.Content.Content;

namespace MVCFramework.Infrastracture.DBConnection

{
    public static partial class EnumExtend
    {
        private static readonly Dictionary<TextDBName, Type> dictionary
            = new Dictionary<TextDBName, Type>()
                {
                    { TextDBName.TextFile, typeof(TextFilesList) } ,
                };

        public static Type GetEntityType(this TextDBName textDBName)
        {
            return dictionary[textDBName];
        }
    }
}