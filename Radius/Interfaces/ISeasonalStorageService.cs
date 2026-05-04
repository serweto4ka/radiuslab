using System;
using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface ISeasonalStorageService
{
    void StartStorage(SeasonalStorage storage);
    SeasonalStorage? GetStorageById(Guid storageId);
    IEnumerable<SeasonalStorage> GetCustomerStorages(Guid customerId);
    IEnumerable<SeasonalStorage> GetActiveStorages();
    void ChangeTireDescription(Guid storageId, string description);
    void ChangeTireQuantity(Guid storageId, int quantity);
    void ChangeExpirationDate(Guid storageId, DateTime newExpirationDate);
    void EndStorage(Guid storageId);
}
