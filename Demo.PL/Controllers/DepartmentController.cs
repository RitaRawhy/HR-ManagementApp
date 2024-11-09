using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(
            //IDepartmentRepository departmentRepository
            IUnitOfWork unitOfWork
            )
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            //1.ViewData => key-value dictionary , .Net Framewor 3.5
            ViewData["Message1"] = "Hello From Department Controller ! [ViewData]";

            //2.ViewBag => dynamic propert (object) , .Net Framework 4
            ViewBag.Message2 = "Hello From Department Controller ! [ViewBag]";

            TempData.Keep("Message3");
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if(ModelState.IsValid)
            {
                await _unitOfWork.DepartmentRepository.Add(department);
                //3.TempData
                TempData["Message3"] = "Hello From Department Controller ! [create=>index][TempData]";
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();
            var department =await _unitOfWork.DepartmentRepository.Get(id);
            if (department is null)
                return NotFound();
            return View(department);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
                return NotFound();
            var department = await _unitOfWork.DepartmentRepository.Get(id);
            if (department is null)
                return NotFound();
            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id , Department department)
        {
            if (id != department.Id)
                return NotFound();
            if(ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.DepartmentRepository.Update(department);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return View(department);
                }
            }
            return View(department);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();
            var department = await _unitOfWork.DepartmentRepository.Get(id);
            if (department is null)
                return NotFound();
            await _unitOfWork.DepartmentRepository.Delete(department);
            return RedirectToAction("Index");
        }
    }
}
