using System;
using System.Collections.Generic;

namespace ApiCore.Models;

public partial class Documentosplantilla
{
    public int IdDocumentosPlantilla { get; set; }

    public string? Tipo { get; set; }

    public string? Nombre { get; set; }

    public string? DocumentosPlantillacol { get; set; }

    public string? Xml { get; set; }

    public string? Original { get; set; }
}
