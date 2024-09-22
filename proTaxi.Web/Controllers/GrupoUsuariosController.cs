using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proTaxi.Domain.Entities;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.GrupoUsuarios;
using proTaxi.Web.Models.GrupoUsuarios;

namespace proTaxi.Web.Controllers
{
    public class GrupoUsuariosController : Controller
    {
        private readonly IGrupoUsuariosRepository grupoUsuariosRepository;


        #region"Acciones"
        public GrupoUsuariosController(IGrupoUsuariosRepository grupoUsuariosRepository)
        {
            this.grupoUsuariosRepository = grupoUsuariosRepository;
        }
        // GET: GrupoUsuariosController
        public async Task<IActionResult> Index()
        {
            var result = await this.grupoUsuariosRepository.GetGruposUsuarios();

            return View(result);
        }

        // GET: GrupoUsuariosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GrupoUsuariosModel model;
            model = await GetGrupoUsuariosInfo(id);
            if (model is null)
            {
                return View();
            }
            return View(model);
        }



        // GET: GrupoUsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GrupoUsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GrupoUsuariosSaveDto saveDto)
        {
            try
            {
                saveDto.ChangeDate = DateTime.Now;
                saveDto.ChangeUser = 1;

                GrupoUsuarios grupoUsuarios = new GrupoUsuarios()
                {
                    Name = saveDto.Name,
                    CreationDate = saveDto.ChangeDate,
                    CreationUser = saveDto.ChangeUser
                };
                var result = await this.grupoUsuariosRepository.Save(grupoUsuarios);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error creando el Grupo de usuarios";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: GrupoUsuariosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GrupoUsuariosModel model = await GetGrupoUsuariosInfo(id);

            if (model is null)
            {
                return View();
            }
            return View(model);
        }

        // POST: GrupoUsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GrupoUsuariosUpdateDto updateDto)
        {
            try
            {
                updateDto.ChangeDate = DateTime.Now;
                updateDto.ChangeUser = 1;

                GrupoUsuarios grupoUsuarios = new GrupoUsuarios()
                { 
                    Id = updateDto.Id,
                    Name = updateDto.Name,
                    ModifyDate = updateDto.ChangeDate,
                    ModifyUser = updateDto.ChangeUser
                };
                var result = await this.grupoUsuariosRepository.Update(grupoUsuarios);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error modificando el Grupo de usuarios";
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
        private async Task<GrupoUsuariosModel> GetGrupoUsuariosInfo(int id)
        {
            GrupoUsuariosModel model = new GrupoUsuariosModel();
            var grupoUsuarios = await this.grupoUsuariosRepository.GetEntityBy(id);

            model = new GrupoUsuariosModel()
            {
                Id = grupoUsuarios.Id,
                Name = grupoUsuarios.Name,
            };
            return model;
        }
        #endregion


    }
}
