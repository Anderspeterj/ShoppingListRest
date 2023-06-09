using ShoppingLib;

namespace ShoppingListRest.Repositories
{
    public class ShoppingItemsRepository
    {
        private int _nextId;
        private List<ShoppingItem> shoppingItems;

        public ShoppingItemsRepository()
        {
            _nextId = 1;
            shoppingItems = new List<ShoppingItem>()
            {
                new ShoppingItem() {Id = _nextId++, Name = "Milk", Price = 2.5, Quantity = 5},
                new ShoppingItem() {Id = _nextId++, Name = "Bread", Price = 1.5, Quantity = 2},
                new ShoppingItem() {Id = _nextId++, Name = "Butter", Price = 3.5, Quantity = 1},

            };
        }

        public List<ShoppingItem> GetAll()
        {
            return new List<ShoppingItem>(shoppingItems);
        }

        public List<ShoppingItem> GetFiltered(int? price, string? namefilter)
        {
            List<ShoppingItem> result = new List<ShoppingItem>(shoppingItems);

            if (namefilter != null)
            {
                result = result.FindAll(shoppingItem => shoppingItem.Name.Contains(namefilter, StringComparison.InvariantCultureIgnoreCase));
            }

            if (price != null)
            {
                int castAmount = (int)price;
                result = result.Take(castAmount).ToList();
            }

            return result;
        }


        public ShoppingItem Add(ShoppingItem item)
        {
            item.Id = _nextId++;
            shoppingItems.Add(item);
            return item;
        }

        public ShoppingItem? Delete(int id)
        {
            ShoppingItem? foundShoppingItem = shoppingItems.Find(item => item.Id == id);
            if (foundShoppingItem == null)
            {
                return null;
            }
            shoppingItems.Remove(foundShoppingItem);
            return foundShoppingItem;

        }


        public double TotalPrice()
        {
            double totalPrice = 0;
            foreach (var item in shoppingItems)
            {
                totalPrice += item.Price * item.Quantity;
            }
            return totalPrice;
        }

    }
}
