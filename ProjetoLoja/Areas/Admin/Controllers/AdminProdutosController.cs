﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Context;
using ProjetoLoja.Models;
using ReflectionIT.Mvc.Paging;

namespace ProjetoLoja.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class AdminProdutosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AdminProdutosController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: Admin/AdminProdutos
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var Resultado = _context.Produtos.Include(i=> i.Categoria).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                Resultado = Resultado.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(Resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }


        // GET: Admin/AdminProdutos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Admin/AdminProdutos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");            
            return View();
        }

        // POST: Admin/AdminProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,Nome,Preco,DescricaoCurta,DescricaoLonga,Quantidade,DataCompra,ValorPago,ImagemUrl,ImagemThumbUrl,IsProdutoPreferido,EmEstoque,CategoriaId,Imagem")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileName(produto.Imagem.FileName);
                string path = Path.Combine(wwwrootPath + "/Imagens/Produtos", fileName);
                produto.ImagemUrl = fileName;
                using (var Stream = new FileStream(path, FileMode.Create))
                {
                    await produto.Imagem.CopyToAsync(Stream);
                }


                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Admin/AdminProdutos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Admin/AdminProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,Nome,Preco,DescricaoCurta,DescricaoLonga,Quantidade,DataCompra,ValorPago,ImagemUrl,ImagemThumbUrl,IsProdutoPreferido,EmEstoque,CategoriaId,Imagem")] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileName(produto.Imagem.FileName);
                    string path = Path.Combine(wwwrootPath + "/Imagens/Produtos", fileName);
                    produto.ImagemUrl = fileName;
                    using (var Stream = new FileStream(path, FileMode.Create))
                    {
                        await produto.Imagem.CopyToAsync(Stream);
                    }
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Admin/AdminProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Admin/AdminProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'AppDbContext.Produtos'  is null.");
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
          return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}
