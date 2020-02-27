using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechnicalAdvisor.Areas.Identity.Data;
using TechnicalAdvisor.Models;
using TechnicalAdvisor.Models.PublicationNonDBModels;
using TechnicalAdvisor.Models.ViewModels;
using TechnicalAdvisor.Services;

namespace TechnicalAdvisor.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly TechnicalAdvisorContext _context;
        private readonly UserService _userService;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly ProductService _productService;
        private readonly XMLService _xMLService;

        public ProductsController(
            TechnicalAdvisorContext context, 
            UserService userService, 
            UserManager<AppIdentityUser> userManager, 
            ProductService productService, 
            XMLService xMLService)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
            _productService = productService;
            _xMLService = xMLService;
        }






        // GET: Products <-- Lista com todos os produtos disponíveis para o usuário visualizar os manuais.
        public async Task<IActionResult> Index()
        {
            //var emailToConfirm = this.User.Identity.Name;

            //var user = _userService.CheckAccessLevel(emailToConfirm);

            //var company = user.Dealer.Company;

            //try
            //{
            //    return View(await _context.Product.Where(p => p.Company == company).ToListAsync());
            //}
            //catch
            //{
            //    return RedirectToAction(nameof(Sorry)); // arrumar essa página, tá dando erro!

            //}


            var productInfos = await _context.Product.ToListAsync();
            ProductIndexViewModel productIndexVIew = new ProductIndexViewModel();
            foreach(var item in productInfos)
            {

                productIndexVIew.Id = item.Id;
                productIndexVIew.Name = item.Name;
                productIndexVIew.TypeOfProduct = item.TypeOfProduct;
                productIndexVIew.PublicationCode = item.PublicationCode;
                productIndexVIew.PublicationVersion = item.PublicationVersion;

                
                //var xmlProduct = _context.XmlProduct.First(x => x.ProductId == item.XmlProductId);
               // string imagePath = xmlProduct.LinkDaImagem;
               // productIndexVIew.ImagePath = imagePath;
            }


            //return View(productIndexVIew);

            return View(await _context.Product.ToListAsync());

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TypeOfProduct,PublicationCode,PublicationVersion")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeOfProduct,PublicationCode,PublicationVersion")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        public IActionResult Sorry()
        {
            return View();
        }

        public IActionResult ShowPublication(int id)
        {
            var page = _productService.TakeIt(_productService.XmlObjectByProductId(id));
            PublicationProductViewModel publication = new PublicationProductViewModel();
            int currentPage = 1;
            publication.NumberOfPage = currentPage;
            publication.Name = page.Name;
            publication.Sections = page.Sections;
            publication.Chapters = page.Chapters;
            publication.Paragraphs = page.Paragraphs;
            var text = page.Paragraphs.Where(p => p.NumberOfPage == currentPage).ToList();
            Concat concat = new Concat(text);
            var texts = concat.WriteText(text);
            publication.Texts = texts;

            var json = JsonConvert.SerializeObject(page);

            publication.json = json;
            

            return View(publication);

            //return RedirectToAction(nameof(NextPage), new { id=id, json=json});
        }


        
        public IActionResult NextPage(int id, string json)
        {
            
            
            var page = (Manual)JsonConvert.DeserializeObject(json);

            page.CurrentPage++;
            PublicationProductViewModel publications = new PublicationProductViewModel();
            int currentPage = page.CurrentPage; 
            publications.NumberOfPage = currentPage;
            publications.Name = page.Name;
            publications.Sections = page.Sections;
            publications.Chapters = page.Chapters;
            publications.Paragraphs = page.Paragraphs;
            var text = page.Paragraphs.Where(p => p.NumberOfPage == currentPage).ToList();
            Concat concat = new Concat(text);
            var texts = concat.WriteText(text);
            publications.Texts = texts;

            var _json = JsonConvert.SerializeObject(page);
            publications.json = _json;

            return View(publications);

            //return RedirectToAction(nameof(Pagination));
        }

        ////POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult NextPage(PublicationProductViewModel publications)
        //{

        //    var page = (Manual)JsonConvert.DeserializeObject(publications.Json);

        //    PublicationProductViewModel publication = new PublicationProductViewModel();
        //    int currentPage = 1;
        //    publication.NumberOfPage = currentPage;
        //    publication.Name = page.Name;
        //    publication.Sections = page.Sections;
        //    publication.Chapters = page.Chapters;
        //    publication.Paragraphs = page.Paragraphs;
        //    var text = page.Paragraphs.Where(p => p.NumberOfPage == currentPage).ToList();
        //    Concat concat = new Concat(text);
        //    var texts = concat.WriteText(text);
        //    publication.Texts = texts;

        //    // var json = JsonConvert.SerializeObject(page);


        //    return View(publication);

        //    //return RedirectToAction(nameof(Pagination));
        //}
















        //public IActionResult Pagination(string json)
        //{
        //    var page = (Manual)JsonConvert.DeserializeObject(json);


        //    PublicationProductViewModel publication = new PublicationProductViewModel();
        //    int currentPage = 1;
        //    publication.NumberOfPage = currentPage;
        //    publication.Name = page.Name;
        //    publication.Sections = page.Sections;
        //    publication.Chapters = page.Chapters;
        //    publication.Paragraphs = page.Paragraphs;
        //    var text = page.Paragraphs.Where(p => p.NumberOfPage == currentPage).ToList();
        //    Concat concat = new Concat(text);
        //    var texts = concat.WriteText(text);
        //    publication.Texts = texts;
           
            
            
        //    return View(publication);
        //}




        ////POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ShowPublication(PublicationProductViewModel publications)
        //{

        //    var page = (Manual)JsonConvert.DeserializeObject(publications.Json);

        //    PublicationProductViewModel publication = new PublicationProductViewModel();

        //    publication.NumberOfPage = 1;
        //    publication.Sections = page.Sections;
        //    publication.Chapters = page.Chapters;
        //    publication.Paragraphs = page.Paragraphs;

        //    return View(publication);

        //}

        ////GET
        //public IActionResult ShowPublication(PublicationProductViewModel publicationProductViewModel)
        //{
        //    bool whatToDo = true; //só pra testar-lhe!!!
        //    PublicationProductViewModel view = new PublicationProductViewModel();
        //    if (whatToDo == true)
        //    {
        //        view.Page = publicationProductViewModel.Page;
        //        view.Page++;
        //        var currentPage = publicationProductViewModel.Paragraphs.
        //      Where(p => p.NumberOfPage == view.Page).ToList();
        //        view.Paragraphs = currentPage;
        //        view.Sections = publicationProductViewModel.Sections;

        //        view.Name = publicationProductViewModel.Name;
        //    }
        //    else
        //    {
        //        view.Page = publicationProductViewModel.Page;
        //        view.Page--;
        //        var currentPage = publicationProductViewModel.Paragraphs.
        //      Where(p => p.NumberOfPage == view.Page).ToList();
        //        view.Paragraphs = currentPage;
        //        view.Sections = publicationProductViewModel.Sections; 

        //        view.Name = publicationProductViewModel.Name;
        //    }

        //    return View(view);
        //}











        //GET
        public IActionResult LoadXML(int productId)
        {

            //Gera view para colocar os dados do produto e carregar o xml
            
            LoadProductXMLFormViewModel loadProductXMLFormViewModel = new LoadProductXMLFormViewModel {
                ID = productId,
               
            };

            return View(loadProductXMLFormViewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoadXML(LoadProductXMLFormViewModel loadProductXMLFormViewModel)
        {


            //Pega arquivo xml e carrega infos pro banco

            _productService.LoadXML(loadProductXMLFormViewModel);

            //Redireciona para a página index de produtos
            return RedirectToAction(nameof(Index));
            
        }

      


    }
}
