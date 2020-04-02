using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Request
{
    public class ClienteRequest
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Cedula { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public List<EmpresaRequest> Empresa { get; set; }

        public static void ProcedureCliente(ClienteRequest cliente, Taller_SysEntities db)
        {
            var dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("nombre", typeof(string));
            dt.Columns.Add("cedJuridica", typeof(string));
            dt.Columns.Add("direccion", typeof(string));
            dt.Columns.Add("telefono", typeof(string));
            dt.Columns.Add("idCliente", typeof(int));

            int i = 1;
            foreach (var oElement in cliente.Empresa)
            {
                dt.Rows.Add(i, oElement.NombreEmpresa, oElement.CedJuridica, oElement.Direccion, oElement.TelEmpresa, i);
                i++;
            }

            var parametros = new SqlParameter("@Negocio", SqlDbType.Structured)
            {
                Value = dt,
                TypeName = "dbo.typ_negocio"
            };


            db.Database.ExecuteSqlCommand("exec Sp_AddCliente @nombre, @apellidos,@cedula,@telefono,@correo, @Negocio"
               , new SqlParameter("@nombre", cliente.Nombre),
               new SqlParameter("@apellidos", cliente.Apellidos),
               new SqlParameter("@cedula", cliente.Cedula),
               new SqlParameter("@telefono", cliente.Telefono),
               new SqlParameter("@correo", cliente.Correo), parametros);

        
        }
    }
}