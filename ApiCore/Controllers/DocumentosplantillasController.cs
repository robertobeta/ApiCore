using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCore.Models;
using ApiCore.ViewModels;
using ApiCore.Utileries;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosplantillasController : ControllerBase
    {
        private readonly VueDbContext _context;

        public DocumentosplantillasController(VueDbContext context)
        {
            _context = context;
        }

        // GET: api/Documentosplantillas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documentosplantilla>>> GetDocumentosplantillas()
        {
            List<MensajesViewModel> mensajes = new List<MensajesViewModel>();

          if (_context.Documentosplantillas == null)
          {
              return NotFound();
          }

          return await _context.Documentosplantillas.ToListAsync();
        }

        // GET: api/Documentosplantillas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documentosplantilla>> GetDocumentosplantilla(int id)
        {
          if (_context.Documentosplantillas == null)
          {
              return NotFound();
          }
            var documentosplantilla = await _context.Documentosplantillas.FindAsync(id);

            if (documentosplantilla == null)
            {
                return NotFound();
            }

            return documentosplantilla;
        }

        // PUT: api/Documentosplantillas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentosplantilla(int id, Documentosplantilla documentosplantilla)
        {
            if (id != documentosplantilla.IdDocumentosPlantilla)
            {
                return BadRequest();
            }

            _context.Entry(documentosplantilla).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentosplantillaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Documentosplantillas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Documentosplantilla>> PostDocumentosplantilla(Documentosplantilla documentosplantilla)
        {
            List<MensajesViewModel> mensajes = new List<MensajesViewModel>();
            if (documentosplantilla.Xml == null)
                mensajes.Add(Mensajes.MensajesError("Error"));
            if (documentosplantilla.Original == null)
                mensajes.Add(Mensajes.MensajesError("Error"));
            if(mensajes.Count == 0)
            {
                try
                {
                    _context.Documentosplantillas.Add(documentosplantilla);
                    await _context.SaveChangesAsync();
                    mensajes.Add(Mensajes.Exitoso("Exito"));
                }
                catch
                {
                    return BadRequest();
                }
            }

            return Ok(mensajes);
        }

        // DELETE: api/Documentosplantillas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentosplantilla(int id)
        {
            if (_context.Documentosplantillas == null)
            {
                return NotFound();
            }
            var documentosplantilla = await _context.Documentosplantillas.FindAsync(id);
            if (documentosplantilla == null)
            {
                return NotFound();
            }

            _context.Documentosplantillas.Remove(documentosplantilla);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentosplantillaExists(int id)
        {
            return (_context.Documentosplantillas?.Any(e => e.IdDocumentosPlantilla == id)).GetValueOrDefault();
        }
    }
}
