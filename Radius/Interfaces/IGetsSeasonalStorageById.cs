using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsSeasonalStorageById
{
    SeasonalStorage GetStorageById(Guid storageId);
}