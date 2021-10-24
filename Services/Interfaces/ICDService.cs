using GestiuneCD.Models.DTOs;
using GestiuneCD.Models.Entities;
using GestiuneCD.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GestiuneCD.Services.Interfaces
{
    public interface ICDService<T> where T : BaseEntity
    {
        Task<T> GetItemByIdAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool? orderedByName = false, bool? orderedBySize = false, int? minSpatiuLiber = 0, VitezaInscriptionare? vitezaMaxInscriptionare = null, TipCD? tipCD = null, bool? cuSesiuniDeschise = null);
        Task<ActionResult<T>> UpdateItemAsync(int id, CDUpdateDTO entity);
        Task<ActionResult<T>> CreateItemAsync(CDSetupDTO entity);
        Task<ActionResult<T>> DeleteItemAsync(int id);
    }
}
