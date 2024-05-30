using ApiCore.Models;
using ApiCore.Utileries;
using ApiCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Reflection.Metadata;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerarDocumentoController : ControllerBase
    {
        private readonly VueDbContext _context;
        public GenerarDocumentoController(VueDbContext context){
            _context = context; 
        }

        // GET: api/<GenerarDocumentoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GenerarDocumentoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoc(int id)
        {

            List<MensajesViewModel> mensajes = new List<MensajesViewModel>();

            //OBTENEMOS EL DOCUMENTO DENTRO DE LA TABLA DocumentosPlantillas MEDIANTE UN ID ESPECIFICO Y LO AGREGAA LA VARIABLE documento
            var documento = _context.Documentosplantillas.FirstOrDefault(d => d.IdDocumentosPlantilla == id);
            var form = _context.Formularios.Find(20);
            if (documento == null)
                mensajes.Add(Mensajes.MensajesError("Ese documento no existe"));

            //O
            byte[] documentoBytes = Convert.FromBase64String(documento.Xml);

            // Crear un MemoryStream con el contenido del documento
            MemoryStream memoryStream = new MemoryStream(documentoBytes);
            memoryStream.Seek(0, SeekOrigin.Begin);
            MemoryStream ouput = new MemoryStream();

            memoryStream.CopyTo(ouput);
            ouput.Seek(0, SeekOrigin.Begin);

            var name = documento.Nombre;

            using (WordprocessingDocument worDocument = WordprocessingDocument.Open(ouput, true))
            {
                string docText = null;
                using (StreamReader reader = new StreamReader(worDocument.MainDocumentPart.GetStream()))
                {
                    docText = reader.ReadToEnd();
                }

               docText = Replacement.Reemplazo(docText, form);

                using (StreamWriter writer = new StreamWriter(worDocument.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    writer.Write(docText);
                }

            }
            return DescargaArchivoWord(ouput, name);
            // Devolver el documento como archivo para su descarga
        }

        [NonAction]
        public FileResult DescargaArchivoWord(Stream file, string DocumentName)
        {
            var mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            file.Seek(0, SeekOrigin.Begin);
            return File(file, mimeType, DocumentName + ".docx");
        }
        [NonAction]
       



        // POST api/<GenerarDocumentoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GenerarDocumentoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GenerarDocumentoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
