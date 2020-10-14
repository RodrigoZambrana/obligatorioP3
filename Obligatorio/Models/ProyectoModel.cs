using Dominio;
using Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio.Models
{
    public class ProyectoModel
    {
        #region Properties
        [Required]
        public int id { get; set; }
        [Required]
        public string titulo { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        public decimal monto { get; set; }
        [Required]
        public string rutaImagen { get; set; }
        public string tipo { get; set; }
        public string experiencia { get; set; } = "";
        public int cantidadIntegrantes { get; set; } = 0;
        public int cuotas { get; set; }
        public System.Web.Mvc.SelectList TodosLosTipos { get; set; }
        public HttpPostedFileBase Archivo { get; set; }
        #endregion
        #region Métodos para preparar el modelo y que tenga lo necesario para desplegar en la vista

        /// <summary>
        /// Obtiene todos los temas y arma una SelectList para pasársela al helper DropDownListFor
        /// </summary>
        /// <remarks>
        /// Los parámetros de SelectList son:
        /// - La colección a desplegar
        /// - El nombre de la property que se utilizará como value
        /// - El nombre de la property que se utilizará para mostrar (text)
        /// --.j
        /// </remarks>

        #endregion
        #region Manejo de la imagen (o cualquier upload de archivo)

        public ProyectoModel()
        {
            this.TodosLosTipos = new SelectList(
      new List<SelectListItem>
      {
        new SelectListItem { Disabled=true, Selected = true, Text = "Seleccione una opcion", Value = "-1"},
        new SelectListItem { Selected = false, Text = "Cooperativo", Value = "Cooperativo"},
        new SelectListItem { Selected = false, Text = "Personal", Value = "Personal"},
    }, "Value", "Text", 1);

        }
            public bool SubirArchivoGuardarNombre ()
        {
            if (this.Archivo != null)
            {
                if (guardarArchivo (Archivo))
                {
                    this.rutaImagen = Archivo.FileName;
                    return true;
                   }
            }
            return false;
        }
        /// <summary>
        /// Guarda en disco el archivo subido mediante upload
        /// </summary>
        /// <param name="archivo">El archivo subido mediante un input file. 
        /// Debe ser un objeto HttpPostedFileBase. Cuidado no confundir con HttpPostedFile </param>
        /// <returns>True si todo funcionó, false en caso contrario.</returns>
        /// <remarks>Deberían capturarse las excepciones.</remarks>
        private bool guardarArchivo ( HttpPostedFileBase archivo )
        {
            if (archivo != null)
            {
                string ruta = System.IO.Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "Images/fotos");
                if (!System.IO.Directory.Exists (ruta))
                    System.IO.Directory.CreateDirectory (ruta);

                ruta = System.IO.Path.Combine (ruta, archivo.FileName);
                archivo.SaveAs (ruta);
                return true;
            }
            else
                return false;
        }
        #endregion

    }
}