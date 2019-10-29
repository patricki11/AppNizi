﻿using System;
using System.Collections.Generic;

namespace AppNiZiAPI.Models.Repositories
{
    interface IConsumptionRepository
    {
        Consumption GetConsumptionByConsumptionId(int consumptionId);
        List<Consumption> GetConsumptionsForPatientBetweenDates(int patientId, DateTime startDate, DateTime endDate);

        int GetConsumptionPatientId(int consumptionId);

        bool AddConsumption(Consumption consumption);

        bool DeleteConsumption(int consumptionId, int patientId);

        bool UpdateConsumption(int consumptionId, Consumption consumption);

    }
}
