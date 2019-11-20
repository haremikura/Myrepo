using MVCFramework.Content.Content;using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XUnitTestProject2
{
    internal class DBContexTestTemplate
    {
        private readonly TextListContena textListContena = new TextListContena();
        private IEnumerable<IEntity> testList;
        public TextEditorContext DbContext { get; set; }

        public DBContexTestTemplate(TextDBName textDbName)
        {
            IEnumerable<IEntity> dataEntity
                = textListContena.GetTextList(textDbName);

            DbContext = GetArticleDbContext();

            switch (textDbName)
            {
                case TextDBName.TextFile:
                    DbContext.TextFilesList.AddRange((IEnumerable<TextFilesList>)dataEntity);
                    DbContext.SaveChanges();
                    testList = DbContext.TextFilesList.ToList();
                    break;

                case TextDBName.ServiceUser:
                    DbContext.ServiceUser.AddRange((IEnumerable<ServiceUser>)dataEntity);
                    DbContext.SaveChanges();
                    testList = DbContext.TextFilesList.ToList();
                    break;
            }
        }

        public string ShowList()
        {
            var IEmutableType = testList.GetType().GenericTypeArguments[0];
            var properties = IEmutableType.GetProperties();

            StringBuilder result = new StringBuilder();

            result.Append($"\r");
            foreach (var prop in properties)
            {
                result.Append($"{prop.Name}");
            }
            result.Append($"\r");
            foreach (var index in testList)
            {
                foreach (var prop in properties)
                {
                    var entityValue = prop.GetValue(index);
                    if (entityValue == null)
                    {
                        entityValue = "(NoValue)";
                    }
                    result.Append($"{entityValue} ");
                }
                result.Append($"\r");
            }
            return result.ToString();
        }

        public TextEditorContext GetArticleDbContext()
        {
            //var options
            //    = new DbContextOptionsBuilder<TextEditorDbContext>();

            var dbContext = new TextEditorContext();
            return dbContext;
        }    }
}