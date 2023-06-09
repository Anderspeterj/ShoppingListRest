const baseUrl = "https://shoppinglistrest2054.azurewebsites.net/api/shoppingitems"

Vue.createApp({
    data() {
        return{
            shoppingItems: [],
            newItem: {id: 0, name: "", amount: 0, price: 0},
            showForm: false,
            showEditForm: false,
            editItem: {name: "", amount: 0, price: 0},
            editId: 0,
            deleteId: 0,
            deleteMessage: "",
            addmessage: "",
            addData: {name: "", price: 0, quanity: 0},
            totalPrice: 0
        }
    },
    methods: {
        async getShoppingItems() {
            try {
                const response = await axios.get(baseUrl)
                this.shoppingItems = response.data
            }
            catch (ex) {
                alert(ex.message)
            }
        },
        async AddShoppingItem() {
            try{
                const response = await axios.post(baseUrl, this.addData)
                this.addMessage = "response " + response.status + " " + response.statusText
                this.getShoppingItems()
            }
            catch (ex) {
                alert(ex.message)
            }
        },
        sortById() {
            this.shoppingItems.sort((a, b) => a.id - b.id);
        },
        sortByName() {
            this.shoppingItems.sort((a, b) => a.name.localeCompare(b.name));
        },
        sortByPrice() {
            this.shoppingItems.sort((a, b) => a.price - b.price);
        }
        ,
        async DeleteShoppingItem(deleteId){
            const url = baseUrl + "/" + deleteId
            try {
                response = await axios.delete(url)
                this.deleteMessage = response.status + " " + response.statusText
                this.getShoppingItems()
            } catch (ex) {
                alert(ex.message)
            }
        },
        async TotalPrice() {
            try {
              const response = await axios.get(baseUrl + "/totalprice");
              this.totalPrice = response.data;
            } catch (ex) {
              alert(ex.message);
            }
        },
        TotalPriceJava(){
            var total = 0
            for (let i = 0; i < this.shoppingItems.length; i++) {
                total += this.shoppingItems[i].price
            }
            this.totalPrice = total
        }
          
    },
   

}).mount("#app")