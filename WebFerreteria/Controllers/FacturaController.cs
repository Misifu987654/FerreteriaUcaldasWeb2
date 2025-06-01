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
    public class FacturaController : Controller
    {
        private readonly FerreteriaDbContext _context;

        public FacturaController(FerreteriaDbContext context)
        {
            _context = context;
        }

        // GET: Factura
        public async Task<IActionResult> Index()
        {
            var ferreteriaDbContext = _context.Facturas.Include(f => f.Pedido);
            return View(await ferreteriaDbContext.ToListAsync());
        }

        // GET: Factura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturas = await _context.Facturas
                .Include(f => f.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturas == null)
            {
                return NotFound();
            }

            return View(facturas);
        }

        // GET: Factura/Create
        public IActionResult Create()
        {
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id");
            return View();
        }

        // POST: Factura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PedidoId,Fecha,Total")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", facturas.PedidoId);
            return View(facturas);
        }

        // GET: Factura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturas = await _context.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return NotFound();
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", facturas.PedidoId);
            return View(facturas);
        }

        // POST: Factura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PedidoId,Fecha,Total")] Facturas facturas)
        {
            if (id != facturas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturasExists(facturas.Id))
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
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", facturas.PedidoId);
            return View(facturas);
        }

        // GET: Factura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturas = await _context.Facturas
                .Include(f => f.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturas == null)
            {
                return NotFound();
            }

            return View(facturas);
        }

        // POST: Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturas = await _context.Facturas.FindAsync(id);
            if (facturas != null)
            {
                _context.Facturas.Remove(facturas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturasExists(int id)
        {
            return _context.Facturas.Any(e => e.Id == id);
        }
    }
}
