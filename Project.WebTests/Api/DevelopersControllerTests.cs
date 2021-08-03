using Microsoft.VisualStudio.TestTools.UnitTesting;

using Project.Domain.Context;
using Project.Domain.Developers;

using System;

namespace Project.Web.Api.Tests
{
    [TestClass()]
    public class DevelopersControllerTests
    {
        Developer dev = new Developer
        {
            BirthDate = DateTime.Today,
            Hobby = "execuatr testes",
            Name = "nome",
            Sex = Sex.Other
        };

        private static readonly IDataBaseContext dataBase;
        private readonly DevelopersController _controller = new DevelopersController(dataBase);

        [TestMethod()]
        public void GetDevelopersTest()
        {
            var result = _controller.GetDevelopers(null, "", "", 0, 10);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetDeveloperTest()
        {
            var result = _controller.GetDeveloper(1);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void NewDeveloperTest()
        {
            var result = _controller.NewDeveloper(dev);

            Assert.Equals(dev, result);
        }

        [TestMethod()]
        public void UpdateDeveloperTest()
        {
            var result = _controller.UpdateDeveloper(1, dev);

            Assert.Equals(dev, result);
        }

        [TestMethod()]
        public void DeleteDeveloperTest()
        {
            var result = _controller.DeleteDeveloper(1);

            Assert.IsNotNull(result);
        }
    }
}