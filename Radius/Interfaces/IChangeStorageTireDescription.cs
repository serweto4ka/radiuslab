using System;

namespace Radius.Interfaces;

public interface IChangeStorageTireDescription
{
    void ChangeTireDescription(Guid storageId, string newDescription);
}