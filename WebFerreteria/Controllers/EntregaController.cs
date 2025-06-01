using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFerreteria.Models;

namespace WebFerreteria.Controllers
{
    public class EntregaController : Controller
    {
        private readonly FerreteriaDbContext _context;

        public EntregaController(FerreteriaDbContext context)
        {
            _context = context;
        }

        // GET: Entrega
        public async Task<IActionResult> Index()
        {
            var ferreteriaDbContext = _context.Entregas.Include(e => e.EntregadoPorNavigation).Include(e => e.Pedido).OrderBy(e => e.EntregadoPorNavigation.NombreCompleto);
            return View(await ferreteriaDbContext.ToListAsync());
        }

        // GET: Entrega/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entregas = await _context.Entregas
                .Include(e => e.EntregadoPorNavigation)
                .Include(e => e.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entregas == null)
            {
                return NotFound();
            }

            return View(entregas);
        }

        // GET: Entrega/Create
        public IActionResult Create()
        {
            ViewData["EntregadoPor"] = new SelectList(_context.Usuarios, "Id", "NombreCompleto");
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id");
            return View();
        }

        // POST: Entrega/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PedidoId,EntregadoPor,DireccionEntrega,FechaEntrega")] Entregas entregas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entregas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntregadoPor"] = new SelectList(_context.Usuarios, "Id", "NombreCompleto", entregas.EntregadoPor);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "NombreCompleto", entregas.PedidoId);
            return View(entregas);
        }

        // GET: Entrega/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entregas = await _context.Entregas.FindAsync(id);
            if (entregas == null)
            {
                return NotFound();
            }
            ViewData["EntregadoPor"] = new SelectList(_context.Usuarios, "Id", "NombreCompleto", entregas.EntregadoPor);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", entregas.PedidoId);
            return View(entregas);
        }

        // POST: Entrega/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PedidoId,EntregadoPor,DireccionEntrega,FechaEntrega")] Entregas entregas)
        {
            if (id != entregas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entregas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntregasExists(entregas.Id))
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
            ViewData["EntregadoPor"] = new SelectList(_context.Usuarios, "Id", "Id", entregas.EntregadoPor);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", entregas.PedidoId);
            return View(entregas);
        }

        // GET: Entrega/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entregas = await _context.Entregas
                .Include(e => e.EntregadoPorNavigation)
                .Include(e => e.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entregas == null)
            {
                return NotFound();
            }

            return View(entregas);
        }

        // POST: Entrega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entregas = await _context.Entregas.FindAsync(id);
            if (entregas != null)
            {
                _context.Entregas.Remove(entregas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntregasExists(int id)
        {
            return _context.Entregas.Any(e => e.Id == id);
        }
    }
}
