﻿using System;
using WebPatversme.Models;
using System.Collections.Generic;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IAnimalsRepository _animalsRepository;

        public AnimalsService(
            IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public List<Animals> AnimalsTable()
        {
            return _animalsRepository.GetAllAnimals();
        }

        public void AddNewAnimal(Animals animal)
        {
            animal.AnimalID = Guid.NewGuid().ToString();
            animal.DateAdded = DateTime.Now;

            _animalsRepository.CreateNewAnimal(animal);
        }
    }
}