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
                //string imagePath = xmlProduct.LinkDaImagem;
                //productIndexVIew.ImagePath = imagePath;
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeOfProduct,PublicationCode,PublicationVersion,PictureName")] Product product)
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

        public IActionResult ShowPublication(int id, int NumberOfPage, int TotalPages)
        {

            if (NumberOfPage == 0 && TotalPages == 0)
            {
                var firstPage = BuildPage(id, 1);
                return View(firstPage);
            }








            else
            {
                Pagination pagination = new Pagination();
                pagination.TotalPages = TotalPages;
                if (NumberOfPage <= 0 || TotalPages == 0)
                {
                    var exclude = _context.Pagination.Where(p => p.id != 0);
                    _context.Pagination.RemoveRange(exclude); //limpar DB antes de começar um novo
                    _context.SaveChanges();
                    //pagination.id = id;
                    pagination.ProductId = id;
                    //precisa pegar o numero de paginas total do manual e comparar com a pagina atual, para saber se pode avançar mais ou nao
                    pagination.CurrentPage = 1; //Current page é 1
                }
                else if (NumberOfPage >= TotalPages && TotalPages != 0)
                {
                    pagination.CurrentPage = TotalPages;
                }


                else
                {
                    pagination.CurrentPage = NumberOfPage;
                }

                _context.Pagination.Add(pagination);
                _context.SaveChanges();
                var page = BuildPage(id, pagination.CurrentPage);


                return View(page);
            }
            
        }

      
        //fazer melhorias na paginação!!!

        public IActionResult NextPage(int id, int currentPage)
        {
            var pagination = _context.Pagination.First(i => i.ProductId == id);
            var NumberOfPage = pagination.CurrentPage;
            NumberOfPage++;
            pagination.CurrentPage = NumberOfPage;
            //ViewData["Page"] = "active";
            _context.Pagination.Update(pagination);
            _context.SaveChanges();
            var page = BuildPage(id, NumberOfPage);
            var TotalPages = page.TotalPages;
            return RedirectToAction(nameof(ShowPublication), new { id = id, NumberOfPage = NumberOfPage, TotalPages = TotalPages });

        }

        public IActionResult PreviousPage(int id, int currentPage)
        {
            var pagination = _context.Pagination.First(i => i.ProductId == id);
            var NumberOfPage = pagination.CurrentPage;
            NumberOfPage--;
            pagination.CurrentPage = NumberOfPage;
            _context.Pagination.Update(pagination);
            _context.SaveChanges();
            var page = BuildPage(id, NumberOfPage);
            return RedirectToAction(nameof(ShowPublication), new { id = id, NumberOfPage = NumberOfPage });

        }


        private PublicationProductViewModel BuildPage(int id, int currentPage)
        {
            var page = _productService.FindProductById(id);
            Manual manual = JsonConvert.DeserializeObject<Manual>(page.Json);
            PublicationProductViewModel publication = new PublicationProductViewModel();
            publication.NumberOfPage = currentPage;
            publication.TotalPages = manual.TotalPages;
            publication.Name = manual.Name;
            publication.Sections = manual.Sections;
            publication.Chapters = manual.Chapters;
            publication.Paragraphs = manual.Paragraphs;
            var text = manual.Paragraphs.Where(p => p.NumberOfPage == currentPage).ToList();
            Concat concat = new Concat(text);
            var texts = concat.WriteText(text);
            publication.Texts = texts;
            publication.id = id;

            return (publication);
        }


        //GET
        public IActionResult LoadPublication(int productId)
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
        public IActionResult LoadPublication(LoadProductXMLFormViewModel loadProductXMLFormViewModel)
        {


            //Pega arquivo xml, cria Json e carrega infos pro banco

            _productService.LoadPublication(loadProductXMLFormViewModel);

            //Redireciona para a página index de produtos
            return RedirectToAction(nameof(Index));
            
        }

      


    }
}
