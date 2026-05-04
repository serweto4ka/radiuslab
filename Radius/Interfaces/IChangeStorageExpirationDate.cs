using System;

namespace Radius.Interfaces;

public interface IChangeStorageExpirationDate
{
    void ChangeExpirationDate(Guid storageId, DateTime newExpirationDate);
}