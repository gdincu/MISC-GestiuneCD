﻿using GestiuneCD.Domain;
using GestiuneCD.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestiuneCD.Services.Interfaces
{
    public interface ISesiuneService<T> where T : BaseEntity
    {
        Task<T> GetItemByIdAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync();
        Task<ActionResult<T>> UpdateItemAsync(int id);
        Task<ActionResult<T>> CreateItemAsync(SesiuneSetupDTO entity,decimal? spatiuAditionalOcupat);
        Task<ActionResult<T>> DeleteItemAsync(int id);
        Task<IEnumerable<T>> InchideSesiunileDeschise();
    }
}
