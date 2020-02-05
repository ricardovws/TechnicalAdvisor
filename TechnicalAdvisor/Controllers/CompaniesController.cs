using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalAdvisor.Models;
using TechnicalAdvisor.Models.ViewModels;

namespace TechnicalAdvisor.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly TechnicalAdvisorContext _context;

        private readonly DealerService _dealerService;

        private readonly CompanyService _companyService;

        private readonly ProductService _productService;

        public CompaniesController(TechnicalAdvisorContext context, DealerService dealerService, CompanyService companyService, ProductService productService)
        {
            _context = context;
            _dealerService = dealerService;
            _companyService = companyService;
            _productService = productService;
        }





        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Company.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Company.FindAsync(id);
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.Id == id);
        }


        public IActionResult CreateDealer(int id)
        {

            DealerFormViewModel formDealer = new DealerFormViewModel
            {
                IdCompany = id
            };
            return View(formDealer);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDealer(DealerFormViewModel formDealer, int id)
        {

            Dealer dealer = new Dealer();
            formDealer.IdCompany = id;
            var company = _companyService.FindCompanyById(id);
            dealer.Name = formDealer.Name;
            dealer.Company = company;
            _dealerService.AddDealer(dealer);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateProduct(int id)
        {

            ProductFormViewModel formProduct = new ProductFormViewModel
            {
                IdCompany = id
            };
            return View(formProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(ProductFormViewModel formProduct, int id)
        {

            Product product = new Product();
            formProduct.IdCompany = id;
            var company = _companyService.FindCompanyById(id);
            product.Name = formProduct.Name;
            product.Company = company;
            product.TypeOfProduct = formProduct.TypeOfProduct;
            product.PublicationCode = formProduct.PublicationCode;
            _productService.AddProduct(product);
           
            return RedirectToAction(nameof(Index));
        }
    }
}
