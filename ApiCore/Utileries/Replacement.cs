using ApiCore.Models;
using DocumentFormat.OpenXml.CustomProperties;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ApiCore.Utileries
{
    public class Replacement
    {

        public static String Reemplazo(string docText, Formulario formulario)
        {
            Formulario formulario1 = formulario;

            PropertyInfo[] properties = typeof(Formulario).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(formulario1).ToString()?.ToString() ?? "";

                string pattern = @"\b" + propertyName + @"\b";
                docText = Regex.Replace(docText, pattern, propertyValue);
            }
            return docText;
        }

    }
}
