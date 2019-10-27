using MVCFramework.Content.Content;
using MVCFramework.Infrastracture.Repositries;
using System.Collections.Generic;

namespace MVCFramework.Infrastracture.DBConnection

{
    public class CrudContext
    {
        private readonly Dictionary<CrudEnum, DBProperty> sqlDictonnary;

        public CrudContext()
        {
            InsertPraseHolderRepository insertPrase = new InsertPraseHolderRepository();
            ReaderRepository readerRepository = new ReaderRepository();

            sqlDictonnary
                = new Dictionary<CrudEnum, DBProperty>()
                    {
                        {CrudEnum.GetLogin , new DBProperty(){
                            Query ="SELECT * FROM ServiceUser WHERE UserName = @UserName AND Password = @Password" ,
                            SetPlaseHolder = insertPrase.InsertServerUserHolder,
                            Boolanswer = readerRepository.IsExist}
                        },
                    };
        }

        public DBProperty GetProperty(CrudEnum crudEnum)
            => sqlDictonnary[crudEnum];
    }
}