﻿using CineProject.Models.Domain;
using CineProject.Repositories.Abstract;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineProject.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Verificar si el nombre del género ya existe en el servicio
            if (_genreService.IsGenreNameExists(model.GenreName))
            {
                // Si el nombre del género ya existe, agregar un mensaje de error a TempData
                TempData["msg"] = "¡Error! El nombre del género ya existe.";
                return RedirectToAction(nameof(Add)); // Redirigir de nuevo a la vista de agregar
            }

            // Si no existe, llamar al servicio para agregar el nuevo género
            var result = _genreService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = _genreService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(GenreList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult GenreList()
        {
            var data = this._genreService.List().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _genreService.Delete(id);
            return RedirectToAction(nameof(GenreList));
        }



    }
}