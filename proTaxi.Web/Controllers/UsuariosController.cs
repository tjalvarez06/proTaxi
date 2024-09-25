using Microsoft.AspNetCore.Mvc;
using proTaxi.Domain.Entities;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.Usuario;
using proTaxi.Web.Models.Usuario;

namespace proTaxi.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository UsuarioRepository;


        #region"Acciones"
        public UsuariosController(IUsuarioRepository UsuarioRepository)
        {
            this.UsuarioRepository = UsuarioRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await this.UsuarioRepository.GetUsuarios();

            return View(result);
        }
        public async Task<IActionResult> Details(int id)
        {
            UsuarioModel model;
            model = await GetUsuariosInfo(id);
            if (model is null)
            {
                return View();
            }
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioSaveDto saveDto)
        {
            try
            {
                saveDto.ChangeDate = DateTime.Now;
                saveDto.ChangeUser = 1;

                Usuario Usuario = new Usuario()
                {                      
                    Documento = saveDto.Documento, 
                    Nombre = saveDto.Nombre, 
                    Apellido = saveDto.Apellido,
                    CreationDate = saveDto.ChangeDate,
                    CreationUser = saveDto.ChangeUser,
                    GrupoUsuariosId = saveDto.GrupoUsuariosId,
                };
                var result = await this.UsuarioRepository.Save(Usuario);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error creando el usuario";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            UsuarioModel model = await GetUsuariosInfo(id);

            if (model is null)
            {
                return View();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioUpdateDto updateDto)
        {
            try
            {
                updateDto.ChangeDate = DateTime.Now;
                updateDto.ChangeUser = 1;

                Usuario Usuarios = new Usuario()
                {
                    Id = updateDto.Id,
                    Documento = updateDto.Documento,
                    Nombre = updateDto.Nombre,
                    Apellido = updateDto.Apellido,
                    ModifyDate = updateDto.ChangeDate,
                    ModifyUser = updateDto.ChangeUser
                };
                var result = await this.UsuarioRepository.Update(Usuarios);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error modificando el usuario";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region"Metodos Privados"
        private async Task<UsuarioModel> GetUsuariosInfo(int id)
        {
            UsuarioModel model = new UsuarioModel();
            var Usuario = await this.UsuarioRepository.GetEntityBy(id);

            model = new UsuarioModel()
            {
                Id = Usuario.Id,
                Documento = Usuario.Documento,
                Nombre = Usuario.Nombre,
                Apellido = Usuario.Apellido,
            };
            return model;
        }
        #endregion


    }
}