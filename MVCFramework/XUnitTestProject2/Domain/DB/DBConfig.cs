using System.Collections.Generic;

namespace XUnitTestProject2.Domain.DB
{
    internal class DBConfig
    {
        private readonly Dictionary<CrudEnum, DBProperty> sqlDictonnary;

        public DBConfig()
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
