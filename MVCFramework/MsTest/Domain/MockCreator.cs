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
        private Mock _mockSettiongList;
        private readonly Mock<TextEditorContext> _mockContext = new Mock<TextEditorContext>();
        private IList<IEntity> mockEntityList;
        private List<IEntity> dataEntity;

        public MockCreator()
        {
        }

        public MockCreator(IList<IEntity> dataEntity)
        {
            SetMock(dataEntity); ;
        }

        public void SetMock(IList<IEntity> list)
        {
            mockEntityList = list;

            if (mockEntityList == null)
            {
                throw new Exception("mockEntityList Is Null");
            }

            SetList(mockEntityList[0].GetType().ToString());
        }

        public void SetList(string entityName)
        {
            switch (entityName)
            {
                case "MVCFramework.Models.Entity.ServiceUser":
                    _mockSettiongList = MockDbSet(mockEntityList.Select(x => new ServiceUser(x)).ToList());

                    _mockContext.Setup(m => m.ServiceUser).Returns((DbSet<ServiceUser>)_mockSettiongList.Object);
                    break;

                case "MVCFramework.Models.Entity.TextFilesList":
                    _mockSettiongList = MockDbSet(mockEntityList.Select(x => new TextFilesList(x)).ToList());
                    _mockContext.Setup(m => m.TextFilesList).Returns((DbSet<TextFilesList>)_mockSettiongList.Object);
                    break;

                default:
                    throw new Exception("Cannot crateMock");
            }
        }

        public void SetMockUserSession()
        {
            List<ServiceUser> MockServiceUser = new List<ServiceUser>
            {
                new ServiceUser { UserId =1, UserName="Test Man", Password="testtest" }
            };

            _mockContext
                .Setup(m => m.ServiceUser)
                .Returns(MockDbSet(MockServiceUser.Select(x => new ServiceUser(x))).Object);
        }

        public void SetMockCurrentSession()
        {
            //List<CurrentSession> MockCurrentSession = new List<CurrentSession>
            //{
            //    new CurrentSession { Id ="asdf",CreatedAt =DateTime.Now},
            //};

            //_mockContext
            //    .Setup(m => m.CurrentSession)
            //    .Returns(MockDbSet(MockCurrentSession.Select(x => new CurrentSession(x))).Object);
        }

        public void SetMockTextFilesList()
        {
            List<TextFilesList> MockTextFilesList = new List<TextFilesList>
            {
                new TextFilesList { FileId =3,FileName="testFileList",UserId=1},
            };

            _mockContext.Setup(m => m.TextFilesList).Returns(MockDbSet(MockTextFilesList.Select(x => new TextFilesList(x))).Object);
        }

        public void SetMockEidtText()
        {
            List<EditText> MockEditTextList = new List<EditText>
            {
                new EditText(){ FileId =3, Text="Test is More Time "},
            };

            _mockContext.Setup(m => m.EditText).Returns(MockDbSet(MockEditTextList.Select(x => new EditText(x))).Object);
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