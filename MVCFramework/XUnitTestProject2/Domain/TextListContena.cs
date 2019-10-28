﻿

using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;

using MVCFramework.Models;
using System.Collections.Generic;

namespace XUnitTestProject2
{
    internal class TextListContena
    {
        private Dictionary<TextDBName, IEnumerable<IEntity>> testDictionary;

        public TextListContena()
        {
            testDictionary
                = new Dictionary<TextDBName, IEnumerable<IEntity>>()
                    {
                        {TextDBName.TextFile,GetTextFilesListList() } ,
                        {TextDBName.ServiceUser,GetUserName() } ,
                };
        }

        private IEnumerable<IEntity> GetUserName()
        {
            var testList = new List<ServiceUser>
                    {
                       new ServiceUser() { UserName = "テスト智之", Password = "1234" },
                       new ServiceUser() { UserName = "asdf", Password = "1111" }
                    };

            return testList;
        }

        private IEnumerable<IEntity> GetTextFilesListList()
        {

            return new List<TextFilesList>
                    {
                        new TextFilesList() {FileId = 100, FileName="bellsystem" ,Text="123abc会いう"},
                        new TextFilesList() {FileId = 101, FileName="yamatoHotel",Text="123abc会いう" }
                    };
        }

        public IEnumerable<IEntity> GetTextList(TextDBName dbName)
            => testDictionary[dbName];
    }
}