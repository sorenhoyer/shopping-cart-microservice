using System;

namespace backend {
    public class Product {
        public Product(string name, double price) {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Price = price;
        }

        public Guid Id {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public double Price {
            get;
            set;
        }
    }
}