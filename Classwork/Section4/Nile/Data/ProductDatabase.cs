﻿/*
 * ITSE1430  
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nile.Data
{
    /// <summary>Provides a base implementation of <see cref="IProductDatabase"/>.</summary>
    public abstract class ProductDatabase : IProductDatabase
    {        
        /// <summary>Add a new product.</summary>
        /// <param name="product">The product to add.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The added product.</returns>
        /// <remarks>
        /// Returns an error if product is null, invalid or if a product
        /// with the same name already exists.
        /// </remarks>
        public Product Add ( Product product )
        {
            //Check for null
            //if (product == null)
                //throw new ArgumentNullException(nameof(product));
                product = product ?? throw new ArgumentNullException(nameof(product));

            //Validate product 
            /* product.Validate();
             var errors = product.Validate();
             var error = errors.FirstOrDefault();
             if (error != null)
             {
                 message = error.ErrorMessage;
                 return null;
            };
            */
            // Verify unique product
            var existing = GetProductByNameCore(product.Name);
            if (existing != null)
                throw new Exception("product already exists");
            //{
            //    message = "Product already exists.";
            //    return null;
            //};

           
            return AddCore(product);
        }

        /// <summary>Gets all products.</summary>
        /// <returns>The list of products.</returns>
        public IEnumerable<Product> GetAll ()
        {
            //option 1 linqs
          //  return GetAllCore()
          //      .OrderBy(p => p.Name)
          //      .ThenByDescending(p => p.Id)
          //      .Select(prop => p);

            return from p in GetAllCore()
                  orderby p.Name, p.Id descending
                  select p;
        }        

        /// <summary>Removes a product.</summary>
        /// <param name="id">The product ID.</param>
        public void Remove ( int id )
        {
            //TODO: Return an error if id <= 0
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be grater than zero");

           // if (id > 0)
           // {
                 RemoveCore(id);
           // };
        }

        /// <summary>Edits an existing product.</summary>
        /// <param name="product">The product to update.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The updated product.</returns>
        /// <remarks>
        /// Returns an error if product is null, invalid, product name
        /// already exists or if the product cannot be found.
        /// </remarks>
        public Product Update ( Product product )
        {
           

            //Check for null
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
               
            };

            //Validate product 
            product.Validate();
            //var error = errors.FirstOrDefault();
            //if (error != null)
            //{
             //   throw new ArgumentNullException(nameof(product));
            //   
           // };



            // Verify unique product
            var existing = GetProductByNameCore(product.Name);
            if (existing != null && existing.Id != product.Id)
            {
                throw new ArgumentException("Product Already Exists");
              
            };

            //Find existing
            existing = existing ?? GetCore(product.Id);
            if (existing == null)
            {
                throw new ArgumentException("Product Already Not found", nameof(product));
               
            };

            return UpdateCore(product);
        }

        protected abstract Product AddCore( Product product );
        protected abstract IEnumerable<Product> GetAllCore();
        protected abstract Product GetCore( int id );
        protected abstract void RemoveCore( int id );
        protected abstract Product UpdateCore( Product product );
        protected abstract Product GetProductByNameCore( string name );
    }
}
