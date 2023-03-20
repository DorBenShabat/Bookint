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

public class CompanyController : Controller
{
    private readonly IUnitOfWork _db;
   
    public CompanyController(IUnitOfWork db)
    {
        _db = db;   
    }
    public IActionResult Index()
    {
        return View();
    }

    //GET

    public IActionResult Upsert(int? id)
    {
        Company company = new();

        if (id==null || id == 0)
        {
            return View(company);
        }
        else
        {
           company = _db.Company.GetFirstOrDefault(u => u.Id == id);
            return View(company);
        }

    }


    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult Upsert(Company obj, IFormFile? file) 
    {
        
        if (ModelState.IsValid)
        {
           
        
            if (obj.Id == 0)
            {
                _db.Company.ADD(obj);
                TempData["Success"] = "Company created successfully";
            }
            else
            {
                _db.Company.Update(obj);
                TempData["Success"] = "Company updated successfully";
            }
            _db.Save();
            
            return RedirectToAction("Index");
        }
        
        return View(obj);
    }

   
    #region API Calls
    [HttpGet]
    public IActionResult GetAll() 
    {
        var companyList = _db.Company.GetAll();
        return Json(new {data = companyList});
    }

    //POST
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _db.Company.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return Json(new {success = false, message = "Error while deliting"});
        }


        _db.Company.REMOVE(obj);
        _db.Save();
        return Json(new { success = true, message = "Delete Successful" });

    }
    #endregion
}