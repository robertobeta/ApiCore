using System;
using System.Collections.Generic;

namespace ApiCore.Models;

public partial class Formulario
{
    public int IdFormulario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public string? Correo { get; set; }

    public string? Comentario { get; set; }
}
