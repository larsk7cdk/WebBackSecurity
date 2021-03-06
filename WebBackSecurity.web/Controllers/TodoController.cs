﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBackSecurity.web.Data.Entities;
using WebBackSecurity.web.Data.Repositories;
using WebBackSecurity.web.ViewModels.Todo;

namespace WebBackSecurity.web.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITodoRepository _todoRepository;

        public TodoController(UserManager<IdentityUser> userManager, ITodoRepository todoRepository)
        {
            _userManager = userManager;
            _todoRepository = todoRepository;
        }

            // LIST
            public async Task<IActionResult> Index()
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var entities = await _todoRepository.GetAllByIdAsync(user.Id);

                if (entities.Count == 0)
                    return View("Empty");

                var todos = entities.Select(MapToViewModel);

                return View("Index", todos);
            }

        // DETAILS
        public async Task<IActionResult> Details([Required]int id)
        {
            //if (id == null) return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();


            var entity = await _todoRepository.GetByIdAsync(id);

            if (entity == null) return NotFound();

            var todo = MapToViewModel(entity);

            return View(todo);
        }

        // CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,IsDone")] TodoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var entity = new Todo
            {
                CreatedDateTime = DateTime.Now,
                UserId = user.Id,
                Name = model.Name,
                Description = model.Description,
                IsDone = model.IsDone
            };

            await _todoRepository.CreateAsync(entity);

            return RedirectToAction(nameof(Index));
        }

        // EDIT
        [Authorize(Policy = "TodoPolicyCanEdit")]
        public async Task<IActionResult> Edit([Required]int id)
        {
            var entity = await _todoRepository.GetByIdAsync(id);

            if (entity == null) return NotFound();

            var todo = MapToViewModel(entity);

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Required]int id, [Bind("Id,Name,Description,IsDone")] TodoViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid) return View(model);

            try
            {
                var entity = await _todoRepository.GetByIdAsync(id);
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.IsDone = model.IsDone;

                await _todoRepository.UpdateAsync(entity);
            }

            catch (DbUpdateConcurrencyException)
            {
                var entity = await _todoRepository.GetByIdAsync(id);
                if (entity == null) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [Authorize(Policy = "TodoPolicyCanDelete")]
        public async Task<IActionResult> Delete([Required]int id)
        {
            var entity = await _todoRepository.GetByIdAsync(id);

            if (entity == null) return NotFound();

            var todo = MapToViewModel(entity);

            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _todoRepository.GetByIdAsync(id);
            await _todoRepository.DeleteAsync(entity);

            return RedirectToAction(nameof(Index));
        }

        // MAPPER
        private static TodoViewModel MapToViewModel(Todo entity)
        {
            return new TodoViewModel
            {
                Id = entity.Id,
                CreatedDateTime = entity.CreatedDateTime,
                Name = entity.Name,
                Description = entity.Description,
                IsDone = entity.IsDone
            };
        }
    }
}