using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using BulkyBook.Utility;
using System.Web.Mvc;

namespace BulkyBook.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]

public class ProductController : Controller
{
    private readonly IUnitOfWork _db;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductController(IUnitOfWork db, IWebHostEnvironment hostEnvironment)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
    }
    public IActionResult Index()
    {
        return View();
    }

    //GET

    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _db.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            }),
            CoverTypeList = _db.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };

        if (id==null || id == 0)
        {
            return View(productVM);
        }
        else
        {
            productVM.Product = _db.Product.GetFirstOrDefault(u => u.ID == id );
            return View(productVM);
        }

    }


    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult Upsert(ProductVM obj, IFormFile? file) 
    {
        
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null) 
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads  = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);

                if(obj.Product.URLImage != null) 
                {
                   var oldPathImage = Path.Combine(wwwRootPath, obj.Product.URLImage.TrimStart('\\'));
                    if (System.IO.File.Exists(oldPathImage)) 
                    {
                        System.IO.File.Delete(oldPathImage);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension),FileMode.Create))
                {
                   file.CopyTo(fileStreams);
                }
                obj.Product.URLImage = @"\images\products\" + fileName+ extension;
            }
            if (obj.Product.ID == 0)
            {
                _db.Product.ADD(obj.Product);
            }
            else
            {
                _db.Product.Update(obj.Product);
            }
            _db.Save();
            TempData["Success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        
        return View(obj);
    }

   
    #region API Calls
    [HttpGet]
    public IActionResult GetAll() 
    {
        var productList = _db.Product.GetAll(includeProperties:"Category,CoverType");
        return Json(new {data = productList});
    }

    //POST
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _db.Product.GetFirstOrDefault(u => u.ID == id);
        if (obj == null)
        {
            return Json(new {success = false, message = "Error while deliting"});
        }

        var oldPathImage = Path.Combine(_hostEnvironment.WebRootPath, obj.URLImage.TrimStart('\\'));
        if (System.IO.File.Exists(oldPathImage))
        {
            System.IO.File.Delete(oldPathImage);
        }

        _db.Product.REMOVE(obj);
        _db.Save();
        return Json(new { success = true, message = "Delete Successful" });

    }
    #endregion
}