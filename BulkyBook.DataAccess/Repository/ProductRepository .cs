using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDB _db;
        
        public ProductRepository(ApplicationDB db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDB = _db.Products.FirstOrDefault(u => u.ID == obj.ID);   
            if (objFromDB != null) 
            {
                objFromDB.Title = obj.Title;
                objFromDB.Description = obj.Description;
                objFromDB.CategoryID = obj.CategoryID;
                objFromDB.ISBN = obj.ISBN;
                objFromDB.Price= obj.Price;
                objFromDB.PriceFor50 = obj.PriceFor50;
                objFromDB.PriceFor100 = obj.PriceFor100;
                objFromDB.ListPrice = obj.ListPrice;
                objFromDB.Author = obj.Author;
                objFromDB.CoverTypeID= obj.CoverTypeID;
                if (obj.URLImage != null)
                {
                    objFromDB.URLImage = obj.URLImage; 
                }
            }
        }
    }
}
