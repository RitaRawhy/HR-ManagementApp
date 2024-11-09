using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Mappers;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository ,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue="")
        {
            IEnumerable<Employee> employees;
            if(string.IsNullOrEmpty(SearchValue))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.Search(SearchValue);
            }          
            var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            //var mappedEmployees = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            //var mappedEmployee = new Employee()
            //{
            //    Id=employee.Id,
            //    Name=employee.Name,
            //    Address=employee.Address,
            //    Age=employee.Age,
            //    DepartmentId=employee.DepartmentId,
            //    Email=employee.Email,
            //    HireDate=employee.HireDate,
            //    IsActive=employee.IsActive,
            //    PhoneNumber=employee.PhoneNumber,
            //    Salary=employee.Salary
            //};

            employeeViewModel.ImageUrl = DocumentSettings.UploadFile(employeeViewModel.Image, "Imgs");
            var mappedEmployee = _mapper.Map<Employee>(employeeViewModel);
            if (ModelState.IsValid)
            {
                await _unitOfWork.EmployeeRepository.Add(mappedEmployee);
                return RedirectToAction("Index");
            }
            return View(employeeViewModel);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();
            var employee = await _unitOfWork.EmployeeRepository.Get(id);
            var mappedEmployee = _mapper.Map<EmployeeViewModel>(employee);
            var departmentName = await _unitOfWork.EmployeeRepository.GetDepartmentByEmployeeId(id);
            if (employee is null)
                return NotFound();
            return View(mappedEmployee);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
                return NotFound();
            var employee = await _unitOfWork.EmployeeRepository.Get(id);
            var mappedEmployee = _mapper.Map<EmployeeViewModel>(employee);
            if (employee is null)
                return NotFound();
            return View(mappedEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    employeeViewModel.ImageUrl = DocumentSettings.UploadFile(employeeViewModel.Image, "Imgs");
                    var mappedEmployee = _mapper.Map<Employee>(employeeViewModel);
                    await _unitOfWork.EmployeeRepository.Update(mappedEmployee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(employeeViewModel);
                }
            }
            return View(employeeViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();
            var employee = await _unitOfWork.EmployeeRepository.Get(id);
            if (employee is null)
                return NotFound();
            DocumentSettings.DeleteFile("Imgs", employee.ImageUrl);
            await _unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }
    }
}
