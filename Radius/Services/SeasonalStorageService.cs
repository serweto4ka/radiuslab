using System;
using System.Collections.Generic;
using System.Linq;
using Radius.Interfaces;
using Radius.Models;

namespace Radius.Services;

public class SeasonalStorageService :
    ISeasonalStorageService
{
    private readonly List<SeasonalStorage> _storages = new();

    public void StartStorage(SeasonalStorage storage)
    {
        if (storage != null) _storages.Add(storage);
    }

    public SeasonalStorage GetStorageById(Guid storageId)
    {
        return _storages.FirstOrDefault(s => s.Id == storageId);
    }

    public IEnumerable<SeasonalStorage> GetCustomerStorages(Guid customerId)
    {
        return _storages.Where(s => s.Client != null && s.Client.Id == customerId);
    }

    public IEnumerable<SeasonalStorage> GetActiveStorages()
    {
        return _storages.Where(s => s.IsActive);
    }

    public void ChangeTireDescription(Guid storageId, string newDescription)
    {
        var storage = GetStorageById(storageId);
        if (storage != null) storage.TireDescription = newDescription;
    }

    public void ChangeTireQuantity(Guid storageId, int newQuantity)
    {
        var storage = GetStorageById(storageId);
        if (storage != null) storage.TireQuantity = newQuantity;
    }

    public void ChangeExpirationDate(Guid storageId, DateTime newExpirationDate)
    {
        var storage = GetStorageById(storageId);
        if (storage != null) storage.ExpirationDate = newExpirationDate;
    }

    public void EndStorage(Guid storageId)
    {
        // Щоб завершити зберігання, ми просто ставимо дату завершення на вчорашній день, 
        // тоді IsActive автоматично стане false
        ChangeExpirationDate(storageId, DateTime.Now.AddDays(-1));
    }
}