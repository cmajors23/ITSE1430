using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data.Memory
{
    // Provides an in memory product database
    public class MemoryProductDatabase
    {
        public Product Add ( Product product, out string message)
        {
           if (product == null)
            {
                message = "Product cannot be null";
                return null;
            };

            //validate product
            var error = product.Validate();
            if(!String.IsNullOrEmpty(error))
            {
                message = error;
                return null;
            };



            //Todo verify unique product

            //Add
            var index = FindEmptyProductIndex()
                if (index >= 0)
            {
                message = "Out of memory";
                return null;
            };
            _products[index] = product;

            //edit
            var index = FindEmptyProductIndex()
                if (index >= 0)
            {
                message = "Out of memory";
                return null;
            };

            public MemoryProductDatabase()
            {
                //seed products
                _products = new Product[25];

                product = new Product();
                product.Name = "iPhone X";
                product.IsDiscontinued = true;
                product.Price = 1500;
                _products[0] = product;

                product = new Product();
                product.Name = "Windows Phone";
                product.IsDiscontinued = true;
                product.Price = 15;
                _products[1] = product;

                product = new Product();
                product.Name = "Samsung S8";
                product.IsDiscontinued = true;
                product.Price = 800;
                _products[2] = product;
            }

            public Product[] GetAll ()
            {
                return _products;
            }

            public void Remove ( int id )
            {
                if(id > 0)
                {
                    var index = GetById(id);
                    if (index >= 0)
                        _products[index] = null;
                }
            }

            //FindEmptyProductIndex existing
            var existingIndex = GetById(product.Id);
            if(existingIndex < 0)
            {
                message = "Product not found";
                return null;
            }

            
            _products[index] = product;
        }

        private int GetById( int id )
        {
            for (var index = 0; index < _products.Length; ++index)
            {
                if (_products[index]?.Id == id)
                    return index;
            }

            return -1;
        }

        private int FindEmptyProductIndex()
        {
            for (var index = 0; index < _products.Length; ++index)
            {
                if (_products[index] == null)
                    return index;
            };
        private Product[] _products;
    }
}
