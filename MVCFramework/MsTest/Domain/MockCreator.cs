using Moq;
using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace XUnitTestProject2.Domain
{
    public class MockCreator
    {
        private readonly Mock _mockMyEntityList;

        // private readonly Mock<IDbContext> _mockContext = new Mock<IDbContext>();
        private readonly Mock<TextEditorContext> _mockContext = new Mock<TextEditorContext>();

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

                    SetAnother();
                    break;
                case "MVCFramework.Models.Entity.TextFilesList":
                    _mockMyEntityList = MockDbSet(mockEntityList.Select(x => new TextFilesList(x)).ToList());

                    _mockContext.Setup(m => m.TextFilesList).Returns((DbSet<TextFilesList>)_mockMyEntityList.Object);

                    SetAnother();
                    break;
                default:
                    throw new Exception("Cannot crateMock");
            }



        }
        private void SetAnother()
        {
            List<CurrentSession> MockCurrentSession = new List<CurrentSession>
            {
                new CurrentSession { Id ="asdf",CreatedAt =DateTime.Now},
            };


            _mockContext.Setup(m => m.CurrentSession).Returns(MockDbSet(MockCurrentSession.Select(x => new CurrentSession(x))).Object);

            List<TextFilesList> MockTextFilesList = new List<TextFilesList>
            {
                new TextFilesList { FileId =3,FileName="testFileList",UserId=1},
            };

            _mockContext.Setup(m => m.TextFilesList).Returns(MockDbSet(MockTextFilesList.Select(x => new TextFilesList(x))).Object);
        }


        public Mock<TextEditorContext> GetMockContext()
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
