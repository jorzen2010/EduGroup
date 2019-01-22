﻿using System;
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
    public class CategoryService
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public List<Category> GetCategoryListByParentID(int ParentID)
        {
            var categorys = unitOfWork.categorysRepository.Get(filter: u => u.CategoryParentID == ParentID);
            List<Category> CategoryList = new List<Category>();
            foreach (Category category in categorys)
            {
                CategoryList.Add(category);
            }
            return CategoryList.ToList();

        }

        //构建一个CategoryList的SelectListItem

        public List<SelectListItem> GetCategorySelectList(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            Category root = unitOfWork.categorysRepository.GetByID(id);
            SelectListItem item = new SelectListItem { Text = root.CategoryName, Value = root.ID.ToString() };
            SelectListItem itemDefault = new SelectListItem { Text = "请选择类别", Value = "" };
            items.Add(itemDefault);
            LoopToAppendChildrenSelectListItem(items, item, id);
            return items;
        }

        private string a = "";
        //
        public void LoopToAppendChildrenSelectListItem(List<SelectListItem> items, SelectListItem rootItem, int pid)
        {
            var subItems = GetCategoryListByParentID(int.Parse(rootItem.Value));

            if (subItems.Count > 0)
            {

                foreach (var subItem in subItems)
                {
                    if (subItem.CategoryParentID == pid)
                    {
                        a = "";
                    }

                    SelectListItem Item = new SelectListItem { Text = a + subItem.CategoryName, Value = subItem.ID.ToString() };
                    items.Add(Item);
                    a += "……";
                    LoopToAppendChildrenSelectListItem(items, Item, pid);

                }

            }

        }



        public Category GetCategoryByName(string cname)
        {
            var categorys = unitOfWork.categorysRepository.Get(filter: u => u.CategoryName == cname);

            Category cate = unitOfWork.categorysRepository.GetByID(1);
            if (categorys.Count() > 0)
            {
                cate = categorys.First();
            }
            return cate;
        }

    }
}
