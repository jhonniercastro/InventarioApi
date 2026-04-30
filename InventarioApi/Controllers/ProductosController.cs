using InventarioApi.Data;
using InventarioApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioCCL.API.Controllers;
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class ProductosController : Controller
{
    private readonly AppDbContext _context;
    public ProductosController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet("inventario")]
    public async Task<ActionResult<IEnumerable<Producto>>> ObtenerInventario()
    {
        return await _context.Productos.ToListAsync();
    }

    [HttpPost("movimiento")]
    public async Task<IActionResult> RegistrarMovimiento([FromBody] MovimientoProducto movimiento)
    {
        var producto = await _context.Productos.FindAsync(movimiento.ProductoId);
        if (producto == null)
            return NotFound(new { mensaje = "Producto no encontrado" });

        if (movimiento.Tipo == "entrada")
            producto.Cantidad += movimiento.Cantidad;
        else if (movimiento.Tipo == "salida")
        {
            if (producto.Cantidad < movimiento.Cantidad)
                return BadRequest(new { mensaje = "No hay suficiente stock disponible" });

            producto.Cantidad -= movimiento.Cantidad;
        }
        else
            return BadRequest(new { mensaje = "El tipo de movimiento debe ser 'entrada' o 'salida'" });

        await _context.SaveChangesAsync();
        return Ok(new { mensaje = "Movimiento registrado correctamente" });
    }
}

