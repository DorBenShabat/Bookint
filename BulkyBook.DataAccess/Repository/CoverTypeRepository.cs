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
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDB _db;
        
        public CoverTypeRepository(ApplicationDB db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType obj)
        {
           _db.CoverTypes.Update(obj);
        }
    }
}
