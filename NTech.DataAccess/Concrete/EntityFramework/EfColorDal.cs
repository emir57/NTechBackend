﻿using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfColorDal : EfAsyncBaseRepository<Color>, IColorDal
    {
        public EfColorDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
