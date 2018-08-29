using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acon.Dapper.Data;
using Acon.Dapper.Repositories;

namespace Acon.UnitTest
{
    [TestClass]
    public class UnitTestDb
    {
        private const string connectString="Server=localhost; Database=Fenxue; uid=root;password=acon123";
        private const string providerName = "MySql.Data.MySqlClient";
        private DBSession sessoin;
        private void InitDb()
        {

            sessoin = DBSession.From(providerName, connectString);
        }

        [TestMethod]
        public void TestMethodDB()
        {
            InitDb();
            IRepository<ConsumableEntity, MaterialCategory> repository = new RepositoryBase<ConsumableEntity, MaterialCategory>(new DapperActiveTransactionProvider(sessoin));
            var entity = repository.Single(a => a.Category == MaterialCategory.NewTube);
            //var entity = repository.FirstOrDefault(a => a.Position == 1);
            var ise = entity == null;
        }
    }

    public class ConsumableEntity
    {
        public MaterialCategory Category { get; set; }

        public int Position { get; set; }

        //public virtual IList<ConsumableItemEntity> Items { get; set; }

        public ConsumableEntity()
        {
            //Items = new List<ConsumableItemEntity>();
        }
    }

    public enum MaterialCategory
    {
        Tip,

        NewTube,

        LabelPaper
    }
}
