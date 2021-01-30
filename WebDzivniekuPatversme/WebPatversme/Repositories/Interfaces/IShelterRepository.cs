﻿using WebPatversme.Models;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Repository.Interfaces
{
    public interface IShelterRepository
    {
        List<Shelters> GetAllAnimalShelters();

        void CreateNewAnimalShelter(Shelters newAnimalShelters);
    }
}