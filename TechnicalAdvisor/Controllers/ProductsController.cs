using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalAdvisor.Areas.Identity.Data;
using TechnicalAdvisor.Models;
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


        //GET
        public IActionResult ViewXML(int id)
        {


            // ESSE MÉTODO PRECISA FAZER ISSO!!!
            // procura e pega no DB um elementxml que seja vinculado a esse id, e dps disso pega e puxa o método takeandreadxml do
            // product service, e devolve o manual
            // pega o objecto manual e monta uma viewmodel e joga pra view
            // **************************









            var product =_context.Product.First(x => x.Id == id); //Procurar no DB o produto que tenha esse id
            var productXML =_context.XmlProduct.LastOrDefault(x => x.ProductId == id); //Procurar no DB um xml que seja relativo ao produto que tenha esse id
            PublicationProductViewModel publicationProductViewModel = new PublicationProductViewModel(); //Criar uma viewmodel para inserir os dados tanto do produto, quanto do XML relativo a ele.

            //Agora vou associar os dados dos 2 objetos na viewmodel, e passar eles pra view
            publicationProductViewModel.FileName = productXML.FileName;
            publicationProductViewModel.ProductId = productXML.ProductId;
            publicationProductViewModel.TituloDoBloco = productXML.TituloDoBloco;
            publicationProductViewModel.InfosDiversas = productXML.InfosDiversas;
            publicationProductViewModel.LinkDaImagem = productXML.LinkDaImagem;
            publicationProductViewModel.MaisInfos = productXML.MaisInfos;
            
            return View(publicationProductViewModel);
        }

        ////POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ViewXML(int productId)
        //{
        //    var list = _xMLService.ListOfXmlsPerProduct(productId);

        //    return View();
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
