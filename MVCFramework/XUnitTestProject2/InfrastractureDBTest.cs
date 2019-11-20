using MVCFramework.Models.Entity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using XUnitTestProject2.Domain;

namespace XUnitTestProject2
{
    public class InfrastractureDBTest
    {
        [Fact]
        public void TestPraseHolder()
        {
            List<IEntity> dataEntity = new List<IEntity>()
                    {
                        new TextFilesList (){FileId = 1,FileName= "tesuto", UserId = 1 },
                        new TextFilesList (){FileId = 3, FileName= "tesuto", UserId = 2 },
                    };

            var mockContext = new MockCreator(dataEntity).GetMockContext();

            var testDbset = mockContext.Object.TextFilesList;

            foreach (var entityIndex in testDbset.ToList())
            {
                Debug.WriteLine($"{entityIndex.FileId} {entityIndex.FileName} {entityIndex.UserId}");
            }
            int num = testDbset.Max(index => index.FileId);
            Debug.WriteLine(num.ToString());
            Assert.True(num == 3);
        }
    }
}