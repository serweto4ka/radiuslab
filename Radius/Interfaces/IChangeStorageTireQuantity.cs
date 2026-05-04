using System;

namespace Radius.Interfaces;

public interface IChangeStorageTireQuantity
{
    void ChangeTireQuantity(Guid storageId, int newQuantity);
}