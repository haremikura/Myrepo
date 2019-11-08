using MVCFramework.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsTest.Domain
{
    public static class ViewEntity
    {
        public static string WriteEntityData(IEntity entity)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var propList = entity.GetType().GetProperties();
            stringBuilder.Append($"{entity.GetType().ToString()}\r");
            foreach (var prop in propList)
            {
                stringBuilder.Append(string.Format("{0, 10}", prop.Name));
            }
            stringBuilder.Append("\r");
            foreach (var prop in propList)
            {
                stringBuilder.Append(string.Format("{0, 10}", prop.GetValue(entity, null)));
            }
            return stringBuilder.ToString();
        }

        internal static string WriteEntityData(IEnumerable<IEntity> list)
        {
            return string.Join("\r", list.Select(x => WriteEntityData(x)));
        }


    }
}
