using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class UsuarioService : IService<Usuario>
    {
        private List<Usuario> _usuarios = new List<Usuario>();

        public void Agregar(Usuario usuario)
        {
            usuario.Id = _usuarios.Count + 1;
            usuario.FechaRegistro = DateTime.Now;
            _usuarios.Add(usuario);
        }

        public void Eliminar(int id)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
                _usuarios.Remove(usuario);
        }

        public void Actualizar(Usuario usuario)
        {
            var existente = ObtenerPorId(usuario.Id);
            if (existente != null)
            {
                existente.Nombre = usuario.Nombre;
                existente.Apellido = usuario.Apellido;
                existente.Documento = usuario.Documento;
                existente.Telefono = usuario.Telefono;
                existente.Login = usuario.Login;
                existente.Clave = usuario.Clave;
            }
        }

        public Usuario ObtenerPorId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }

        public List<Usuario> Listar()
        {
            return _usuarios;
        }
    }
}
