using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkyDal;
using SkyEntity;

namespace SkyService
{
    public class CommonService
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public static Category GetCategoryById(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Category cat = unitOfWork.categorysRepository.GetByID(id);

            return cat;
        }
    }
}
