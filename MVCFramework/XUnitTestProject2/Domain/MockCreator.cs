using Moq;
using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace XUnitTestProject2.Domain
{
    public class MockCreator
    {
        private readonly Mock _mockMyEntityList;

        private readonly Mock<IDbContext> _mockContext = new Mock<IDbContext>();

        public MockCreator(IList<IEntity> mockEntityList)
        {
            if (mockEntityList == null)
            {
                throw new Exception("mockEntityList Is Null");
            }
            IEntity entity = mockEntityList[0];
            switch (entity.GetType().ToString())
            {
                case "MVCFramework.Models.Entity.ServiceUser":
                    _mockMyEntityList = MockDbSet(mockEntityList.Select(x => new ServiceUser(x)).ToList());
                    _mockContext.Setup(m => m.ServiceUser).Returns((DbSet<ServiceUser>)_mockMyEntityList.Object);
                    break;
                case "MVCFramework.Models.Entity.TextFilesList":
                    _mockMyEntityList = MockDbSet(mockEntityList.Select(x => new TextFilesList(x)).ToList());
                    _mockContext.Setup(m => m.TextFilesList).Returns((DbSet<TextFilesList>)_mockMyEntityList.Object);
                    break;
                default:
                    throw new Exception("Cannot crateMock");
            }
        }

        public Mock<IDbContext> GetMockContext()
        {
            return _mockContext;
        }

        private Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());
            dbSetMock.Setup(x => x.Create()).Returns(new T());

            return dbSetMock;

        }
    }
}
