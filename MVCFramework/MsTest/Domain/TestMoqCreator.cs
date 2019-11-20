namespace XUnitTestProject2.Domain
{
    internal class TestMoqCreator
    {
        //public TextEditorContext GetArticleDbContextByEF(List<IEntity> list)
        //{
        //    var dataEntity = list.AsQueryable();
        //    // DbSetのMock
        //    var mockMyEntity = new Mock<DbSet<IEntity>>();
        //    // DbSetとテスト用データを紐付け
        //    mockMyEntity.As<IQueryable<IEntity>>().Setup(m => m.Provider).Returns(dataEntity.Provider);
        //    mockMyEntity.As<IQueryable<IEntity>>().Setup(m => m.Expression).Returns(dataEntity.Expression);
        //    mockMyEntity.As<IQueryable<IEntity>>().Setup(m => m.ElementType).Returns(dataEntity.ElementType);
        //    mockMyEntity.As<IQueryable<IEntity>>().Setup(m => m.GetEnumerator()).Returns(dataEntity.GetEnumerator());

        //    // DBContextにMockを設定

        //    Type CurrentType = list.GetType().GetGenericArguments()[0];
        //    var mockContext = new Mock<TextEditorContext>();

        //    switch (CurrentType.ToString())
        //    {
        //        case "ServiceUser":
        //            mockContext.Setup(m => m.ServiceUser).Returns(mockMyEntity.Object);
        //            break;
        //    }
        //    IEntity entity = (IEntity)Activator.CreateInstance(CurrentType);
        //    mockMyEntity.Setup(f => f.Create()).Returns(entity);
        //    return mockContext.Object;
        //}

        //private Expression<Action<TextEditorContext>> Change(Type CurrentType)
        //{
        //    switch (CurrentType.ToString())
        //    {
        //        case "ServiceUser":
        //            return context => context.ServiceUser.GetType();
        //    }
        //    return null;
        //}
    }
}