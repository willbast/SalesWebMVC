using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
using System;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerservice, DepartmentService departmentService)
        {
            _sellerService = sellerservice;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list =  await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departaments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departaments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            //caso o javascript esteja desabilitado o IF abaixo valida se os requisitos foram validados.
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel
                {
                    Departments = departments,
                    Seller = seller
                };
                return View(viewModel);
            };
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id null" });
            }

            var obj =  await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not found Seller" });
            }

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveSellerAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id null" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller not found" });
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id null" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel
                {
                    Departments = departments,
                    Seller = seller
                };
                return View(viewModel);
            };

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde com o Id do Seller" });
            }

            try
            {
               await _sellerService.UpdateAsync(seller);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}