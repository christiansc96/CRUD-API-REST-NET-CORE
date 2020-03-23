using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareaSemana7_E1_ChristianSanchez.DB;
using TareaSemana7_E1_ChristianSanchez.Models;

namespace TareaSemana7_E1_ChristianSanchez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BDContext _context;

        public UsuariosController(BDContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.Include(x=>x.Rol).ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.Include(x => x.Rol).FirstOrDefaultAsync(x=>x.IdUsuario==id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario model)
        {
            if (id != model.IdUsuario)
            {
                return BadRequest();
            }

            var data = await _context.Roles.FirstOrDefaultAsync(x => x.IdRol == model.IdRol);
            if (data == null)
            {
                return NotFound("El rol asignado no existe");
            }

            var rol = await _context.Roles.FirstOrDefaultAsync(x => x.IdRol == model.IdRol);
            if (rol.IdRol != model.IdRol)
            {
                return NotFound("No esta permitido asignarle otro rol al usuario");
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario model)
        {

            var data = await _context.Roles.FirstOrDefaultAsync(x=>x.IdRol==model.IdRol);
            if (data == null)
            {
                return NotFound("El rol asignado no existe");
            }

            _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = model.IdUsuario }, model);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
