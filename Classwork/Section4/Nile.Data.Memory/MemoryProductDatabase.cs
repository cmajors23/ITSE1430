/*
 * ITSE1430  
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nile.Data.Memory
{
    /// <summary>Provides an in-memory product database.</summary>
    public class MemoryProductDatabase : ProductDatabase
    {                
        protected override Product AddCore ( Product product )
        {
            // Clone the object
            product.Id = _nextId++;
            _products.Add(Clone(product));

            // Return a copy
            return product;
        }

        protected override Product GetCore( int id )
        {
            //option 4 combo
            return (from p in _products
                   where p.Id == id
                   select p).FirstOrDefault();

            //option 3 Linq
          //  var items = from p in _products
          //              where p.Id == id
          //              select p;
          //  return items.FirstOrDefault();

            //extension method
            //return _products.FirstOrDefault(prop => prop.Id == id);

            //option 1 sanity
            //for (var index = 0; index < _products.Length; ++index)
           // foreach (var product in _products)
           // {
            //    if (product.Id == id)
             //       return product;
            //};

            //return null;
        }

        protected override IEnumerable<Product> GetAllCore ()
        {

            //option 3 linq
            return from p in _products
                   select Clone(p);

            //extension method
            //return _products.Select(p => Clone(p));
            
            
            //Iterator syntax
           // foreach (var product in _products)
            //{
              //  if (product != null)
                //    yield return Clone(product);
           // };
        }
        
        protected override void RemoveCore ( int id )
        {
            var existing = GetCore(id);
            if (existing != null)
                _products.Remove(existing);
        }

        protected override Product UpdateCore ( Product product )
        {
            var existing = GetCore(product.Id);

            // Clone the object
            Copy(existing, product);

            return product;
        }

        protected override Product GetProductByNameCore( string name )
        {
            //option 3 linq
            return (from p in _products
                 where String.Compare(p.Name, name, true) == 0
                 select p).FirstOrDefault();

            //option 2 extension
            //return _products.FirstOrDefault(p =>
            //String.Compare(p.Name, name, true) == 0);

          //  foreach (var product in _products)
           // {
            //    if (String.Compare(product.Name, name, true) == 0)
             //       return product;
           // };

            //return null;
        }

        #region Private Members

        //Clone a product
        private Product Clone ( Product item )
        {
            var newProduct = new Product();
            Copy(newProduct, item);

            return newProduct;
        }

        //Copy a product from one object to another
        private void Copy ( Product target, Product source )
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.IsDiscontinued = source.IsDiscontinued;
        }
                        
        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;

        #endregion
    }
}
