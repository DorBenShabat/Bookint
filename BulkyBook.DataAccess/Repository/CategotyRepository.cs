﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CategotyRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDB _db;
        
        public CategotyRepository(ApplicationDB db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
           _db.Categories.Update(obj);
        }
    }
}
