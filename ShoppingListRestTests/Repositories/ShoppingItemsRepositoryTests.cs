using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingListRest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingLib;


namespace ShoppingListRest.Repositories.Tests
{
    [TestClass()]
    public class ShoppingItemsRepositoryTests
    {
        

        [TestMethod()]
        public void GetAllTest()
        {
            ShoppingItemsRepository repository = new ShoppingItemsRepository();
            var result = repository.GetAll();
            Assert.AreEqual(3, result.Count);
        }

        

        [TestMethod()]
        public void TotalPriceTest()
        {
            ShoppingItemsRepository repository = new ShoppingItemsRepository();
            var result = repository.TotalPrice();
            Assert.AreEqual(19, result);
        }

        
    }
}