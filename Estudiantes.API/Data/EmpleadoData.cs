using Estudiantes.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Empleados.API.Data
{
    public class EmpleadoData
    {
        private readonly string connection;
        public EmpleadoData(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("CadenaSQL")!;
                
        }

        public async Task<List<Empleado>> Lista()
        {
            List<Empleado> lista = new List<Empleado>();
            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaEmpleados", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString(),
                        });
                    }
                }
            }
            return lista;
        }


        public async Task<Empleado> Obtener(int Id)
        {
            Empleado objeto = new Empleado();
            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = (new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString(),
                        });
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Empleado empleado)
        {
            bool respuesta = true;
            using (var con = new SqlConnection(connection))
            {
                
                SqlCommand cmd = new SqlCommand("sp_crearEmpleado", con);
                cmd.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                cmd.Parameters.AddWithValue("@Sueldo", empleado.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", empleado.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception)
                {
                    respuesta = false;
                    throw;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(Empleado empleado)
        {
            bool respuesta = true;
            using (var con = new SqlConnection(connection))
            {

                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", empleado.IdEmpleado);
                cmd.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                cmd.Parameters.AddWithValue("@Sueldo", empleado.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", empleado.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception)
                {
                    respuesta = false;
                    throw;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int Id)
        {
            bool respuesta = true;
            using (var con = new SqlConnection(connection))
            {

                SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception)
                {
                    respuesta = false;
                    throw;
                }
            }
            return respuesta;
        }
    }
}
