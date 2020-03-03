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
            foreach (var item in productInfos)
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


        //esse é o método para fazer a busca geral de produtos, tanto por nome, quanto por tipo.
        public IActionResult Search(string somethingToSearch)
        {
            //NullReferenceException
            try
            {
                string word = somethingToSearch.ToUpper(); //palavra que está sendo usada para busca convertida pra maiuscula

                //faz uma lista de todos os produtos que o nome ou o tipo de produto são iguais a palavra procurada
                var list = _context.Product.Where(p => p.Name.ToUpper().StartsWith(word)
                ||
                p.TypeOfProduct.ToUpper().StartsWith(word)).ToList();

                return View(list);
            }
            catch(NullReferenceException)
            {
                return RedirectToAction(nameof(NothingFound));
            }
            
        }
       
        public IActionResult NothingFound()
        {

            return View();
        }


        //aqui é o método para realizar busca dentro do manual
        //public IActionResult SearchInManual(int id, string somethingToSearch)
        //{
        //    try
        //    {
        //        //vai procurar primeiro na página atual
        //        var currentPage = _context.Pagination.FirstOrDefault(p => p.ProductId == id).CurrentPage;
        //        var page = BuildPage(id, currentPage);
        //        var paragraphs = page.Texts;
        //        //var lookingFor = paragraphs.IndexOf(somethingToSearch);
        //        var lookingFor = paragraphs.Contains(somethingToSearch);

        //        if //(lookingFor < 0) 
        //        (lookingFor == false)
        //        {
        //            return RedirectToAction(nameof(NothingFound));

        //        }

        //        //método para contar quantas incidencias da palavra buscada foram encontradas na página:

        //        int times = 0;
        //        var texts = paragraphs.Split(' ');

        //        foreach(var text in texts)
        //        {
        //            if
        //                 (text == somethingToSearch)
        //            {

        //                times++;
        //            }


        //           else if (text.EndsWith(","))
        //            {
        //                var littleText = text.Split(",");
        //                foreach(var little in littleText)
        //                {
        //                    var x = littleText[0];
        //                    if (x == somethingToSearch)
        //                    {
        //                        times++;
        //                        break;

        //                    }
        //                }
        //            }

        //            else if (text.EndsWith("."))
        //            {
        //                var littleText = text.Split(".");
        //                foreach (var little in littleText)
        //                {
        //                    var x = littleText[0];
        //                    if (x == somethingToSearch)
        //                    {
        //                        times++;
        //                        break;

        //                    }
        //                }
        //            }

        //        }

        //        SearchInManualViewModel viewModel = new SearchInManualViewModel();

        //        if (times == 0)
        //        {
        //            return RedirectToAction(nameof(NothingFound));
        //        }
        //        else
        //        {

        //            viewModel.NumberPage = currentPage;
        //            viewModel.ChapterTitle = page.Paragraphs.First(f => f.NumberOfPage == currentPage).ChapterTitle;
        //            viewModel.SectionTitle = page.Paragraphs.First(f => f.NumberOfPage == currentPage).SectionTitle;
        //            viewModel.WordSearch = somethingToSearch;
        //            viewModel.Times = times;
        //            return View(viewModel);
        //        }

        //    }

        //    catch (ArgumentNullException)
        //    {
        //        return RedirectToAction(nameof(NothingFound));
        //    }

        //}

        //aqui é o método para realizar busca dentro do manual
        public IActionResult SearchInAllManual(int id, string somethingToSearch)
        {
            //vai procurar primeiro na página atual
            var currentPage = _context.Pagination.FirstOrDefault(p => p.ProductId == id).CurrentPage;
            var page = BuildPage(id, currentPage); //monta a página
            var pages = page.Paragraphs;

            List<SearchInManualViewModel> viewModels = new List<SearchInManualViewModel>();


            ///////////aqui ele começa a procurar dentro de cada paragraph
            foreach (var pag in pages)
            {
                int times = 0;
                var text = pag.Texts;
                try
                {
                var contains = text.Contains(somethingToSearch);
                if (contains == false)
                {
                    //return RedirectToAction(nameof(NothingFound));
                    ;
                }

                else
                {
                    var texts = text.Split(' '); //entra em cada text e separa por espaços em branco
                    foreach (var _text in texts)
                    {
                        if
                        (_text == somethingToSearch) //verifica se cada palavra bate
                        {

                            times++;
                        }

                        else if (_text.EndsWith(","))
                        {
                            if (_text.EndsWith("."))
                                {
                                    var _littleText = _text.Split(".");
                                    foreach (var little in _littleText)
                                    {
                                        var x = _littleText[0];
                                        if (x == somethingToSearch)
                                        {
                                            times++;
                                            break;

                                        }
                                    }
                                }
                            var littleText = _text.Split(",");
                            foreach (var little in littleText)
                            {
                                var x = littleText[0];
                                if (x == somethingToSearch)
                                {
                                    times++;
                                    break;

                                }
                            }
                        }

                        //else if (_text.EndsWith("."))
                        //{
                               
                        //    var littleText = _text.Split(".");
                        //    foreach (var little in littleText)
                        //    {
                        //        var x = littleText[0];
                        //        if (x == somethingToSearch)
                        //        {
                        //            times++;
                        //            break;

                        //        }
                        //    }
                        //}

                    }

                } }
                catch (ArgumentNullException)
                {
                    return RedirectToAction(nameof(NothingFound));
                }
                ///////////termina a busca dentro dos paragraphs
                ///
                SearchInManualViewModel viewModel = new SearchInManualViewModel();
                viewModel.Times = times;
                viewModel.NumberPage = pag.NumberOfPage;
                viewModel.SectionTitle = pag.SectionTitle;
                viewModel.ChapterTitle = pag.ChapterTitle;
                
                viewModel.WordSearch = somethingToSearch;
                viewModels.Add(viewModel);

                ///aqui é para melhorar a forma em que os dados serão enviados para a view. Irá melhorar a exibição também.
                foreach(var model in viewModels)
                {

                    var newModel = viewModels.FirstOrDefault();
                    //viewModels.Remove(newModel);
                    int numberOfPage = newModel.NumberPage;
                    
                    

                }





            }

            bool check = NotFoundHere(viewModels);
            if(check == true)
            {
                return RedirectToAction(nameof(NothingFound));
            }
        
                return View(viewModels);
          
        }

        public bool NotFoundHere(List<SearchInManualViewModel> viewModels)
        {
            var nothing = 0;
            foreach (var view in viewModels)
            {

                if (view.Times == 0)
                {
                    ;
                }
                else
                {
                    nothing++;
                }
            }

            if (nothing == 0)
            {
                return true;
            }

            return false;
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
            Pagination pagination = new Pagination();
            PublicationProductViewModel page = new PublicationProductViewModel();

            if (NumberOfPage == 0 && TotalPages == 0)
            {
                page = ChangePage(pagination, id, 1, TotalPages);  //Current page é 1

                return View(page);
            }


            else if (NumberOfPage <= 0)
            {

                page = ChangePage(pagination, id, 1, TotalPages);  //Current page é 1
                return View(page);
            }

            else if (NumberOfPage >= TotalPages)
            {
              
               page = ChangePage(pagination, id, TotalPages, TotalPages); //Current page é o total de páginas
                return View(page);
            }

            else
            {

                page = ChangePage(pagination, id, NumberOfPage, TotalPages); //Current page é o número da pagina atual
                return View(page);
            }

          
        }


        private PublicationProductViewModel ChangePage(Pagination pagination, int id, int CurrentPage, int TotalPages)
        {
            var exclude = _context.Pagination.Where(p => p.id != 0);
            _context.Pagination.RemoveRange(exclude); //limpar DB antes de começar um novo
            _context.SaveChanges();
            pagination.ProductId = id;
            pagination.TotalPages = TotalPages;
            //precisa pegar o numero de paginas total do manual e comparar com a pagina atual, para saber se pode avançar mais ou nao
            pagination.CurrentPage = CurrentPage; //
            _context.Pagination.Add(pagination);
            _context.SaveChanges();
            var page = BuildPage(id, pagination.CurrentPage);
            return page;

        }


        //fazer melhorias na paginação!!!

        public IActionResult NextPage(int id, int currentPage)
        {
            var pag=_context.Pagination.FirstOrDefault(p => p.CurrentPage != 0);
            Pagination pagination = new Pagination();
            PublicationProductViewModel page = new PublicationProductViewModel();
            var NumberOfPage = pag.CurrentPage;
            if (NumberOfPage == 0)
            {
                NumberOfPage = 1;
            }
            NumberOfPage++;
            if (NumberOfPage <= pag.CurrentPage)
            {
                NumberOfPage = pag.CurrentPage;
            }
            pagination.CurrentPage = NumberOfPage;
            //ViewData["Page"] = "active";
            _context.Pagination.Update(pagination);
            _context.SaveChanges();
            page = BuildPage(id, NumberOfPage);
            var TotalPages = page.TotalPages;
            if (NumberOfPage > TotalPages)
            {
                NumberOfPage = TotalPages;
            }
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
            var TotalPages = page.TotalPages;
            return RedirectToAction(nameof(ShowPublication), new { id = id, NumberOfPage = NumberOfPage, TotalPages = TotalPages });

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
