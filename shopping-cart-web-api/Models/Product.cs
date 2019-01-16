namespace backend {
    public class Product {
        public Product(string Id, double Price) {
            this.Id = Id;
            this.Price = Price;
        }

        public string Id {
            get;
            set;
        }

        public double Price {
            get;
            set;
        }
    }
}