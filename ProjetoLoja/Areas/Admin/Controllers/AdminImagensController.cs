using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjetoLoja.Models;

namespace ProjetoLoja.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminImagensController(IWebHostEnvironment webHostEnvironment, IOptions<ConfigurationImagens> myConfiguration)
        {
            _webHostEnvironment = webHostEnvironment;
            _myConfig = myConfiguration.Value;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files,FileManagerModel imagem)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Erro: Arquivo(s) não Selecionado(s)";
                return View(ViewData);
            }

            if(files.Count > 10)
            {
                ViewData["Erro"] = "Erro: Numero de Arquivos exede o permitido";
                return View(ViewData);
            }

            long size = files.Sum(f => f.Length);
            var filePathName = new List<string>();
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos);
            
            foreach(var formFile in files)
            {
                if(formFile.FileName.Contains(".jpg")|| formFile.FileName.Contains(".gif")|| formFile.FileName.Contains(".png")|| formFile.FileName.Contains(".jpeg"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    filePathName.Add(fileNameWithPath);
                    imagem.ImagemNome = fileNameWithPath;
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} Arquivos foram enviados ao servidor," + $" com tamanho total de: {size} bytes.";
            ViewBag.Arquivos = filePathName;

            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos);


            DirectoryInfo dir = new DirectoryInfo(userImagePath);

            FileInfo[] files = dir.GetFiles();
            model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

            if(files.Length == 0)
            {
                ViewData["ERRO"] = $"Nenhuma imagem encontrada na pasta{userImagePath}";
            }

            model.Files = files;
            return View(model);

        }

        public IActionResult DeleteFile(string fname)
        {
            string _imagemDeleta = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos + "\\",fname);

            if (System.IO.File.Exists(_imagemDeleta))
            {
                System.IO.File.Delete(_imagemDeleta);
                ViewData["DELETADO"] = $" Arquivo(s) {_imagemDeleta} deletado com sucesso";
            }
            return View("Index");
        }
    }
}
