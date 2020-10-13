using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ejemplo_View_Model_2.Models
{
    public class LibroModel
    {
        #region Properties
        public int Id { get; set; }//Solo para cuando se recupera de la BD, no se usa al guardar
        [StringLength (50)]
        public string Titulo { get; set; }
        [DataType (DataType.Date, ErrorMessage = "Debe ingresar una fecha")]
        [DisplayFormat (ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaPublicacion { get; set; }
        [StringLength (50)]
        public string Descripcion { get; set; }
        [StringLength (50, MinimumLength = 5)]
        public string NombreArchivoPortada { get; set; }
        //Propiedad para guardar el tema que se seleccione en la select

        public int IdTema { get; set; }
        //SelectList es soportada por @Html.DropDownListFor
        public System.Web.Mvc.SelectList TodosLosTemas { get; set; }
        //Permite subir el archivo, además de obtener el nombre
        public HttpPostedFileBase Archivo { get; set; }
        #endregion
        #region Métodos para preparar el modelo y que tenga lo necesario para desplegar en la vista
        public LibroModel ()
        {

        }

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
        public bool SubirArchivoGuardarNombre ()
        {
            if (this.Archivo != null)
            {
                if (guardarArchivo (Archivo))
                {
                    this.NombreArchivoPortada = Archivo.FileName;
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