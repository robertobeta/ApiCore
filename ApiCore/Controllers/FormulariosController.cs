using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCore.Models;
using ApiCore.ViewModels;
using System.Linq.Expressions;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MimeKit;
using ApiCore.Utileries;
using System.Net;


namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulariosController : ControllerBase
    {
        private readonly VueDbContext _context;

        public FormulariosController(VueDbContext context)
        {
            _context = context;
        }

        // GET: api/Formularios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Formulario>>> GetFormularios()
        {

          if (_context.Formularios == null)
          {
              return NotFound();
          }
            return await _context.Formularios.ToListAsync();
        }

        // GET: api/Formularios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Formulario>> GetFormulario(int id)
        {
          if (_context.Formularios == null)
          {
              return NotFound();
          }
            var formulario = await _context.Formularios.FindAsync(id);

            if (formulario == null)
            {
                return NotFound();
            }

            return formulario;
        }

        // PUT: api/Formularios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormulario(int id, Formulario formulario)
        {
            if (id != formulario.IdFormulario)
            {
                return BadRequest();
            }

            _context.Entry(formulario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormularioExists(id))
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

        // POST: api/Formularios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Formulario>> PostFormulario(Formulario formulario)
        {
            List<MensajesViewModel> mensajes = new List<MensajesViewModel>();
           
          if (_context.Formularios == null)
              return Problem("Entity set 'VueDbContext.Formularios'  is null.");
          if (String.IsNullOrEmpty(formulario.Correo))
              mensajes.Add(Mensajes.MensajesError("Correo necesario"));
          if (String.IsNullOrEmpty(formulario.Nombre))
               mensajes.Add(Mensajes.MensajesError("Nombre necesario"));
          if(mensajes.Count == 0)
            {
                try
                {
                    _context.Formularios.Add(formulario);
                    await _context.SaveChangesAsync();
                    var body = $"Nombre: {formulario.Nombre}<br>Apellido: {formulario.Apellidos}<br>Correo: {formulario.Correo}<br>Comentario: {formulario.Comentario}";
                    var destinatario = formulario.Correo;
                    var asunto = "Nuevo correo enviado";
                    EnviarCorreo(destinatario, asunto, body);
                    mensajes.Add(Mensajes.Exitoso("Exitoso"));
                }
                catch
                {
                    return BadRequest();
                }
            }
            
            return Ok(mensajes);
        }
        private void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {

            var stmpClient = new System.Net.Mail.SmtpClient("smtp.office365.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("roberto__2022@outlook.com", "antonio20018*"),
                EnableSsl = true
            };
            var message = new MailMessage
            {
                From = new MailAddress("roberto__2022@outlook.com"),
                Subject = asunto,
                Body = cuerpo,
                IsBodyHtml = true,
            };

            message.To.Add(destinatario);
            stmpClient.Send(message);
           
        }
        // DELETE: api/Formularios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormulario(int id)
        {
            if (_context.Formularios == null)
            {
                return NotFound();
            }
            var formulario = await _context.Formularios.FindAsync(id);
            if (formulario == null)
            {
                return NotFound();
            }

            _context.Formularios.Remove(formulario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormularioExists(int id)
        {
            return (_context.Formularios?.Any(e => e.IdFormulario == id)).GetValueOrDefault();
        }
    }
}
