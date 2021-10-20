using GestiuneCD.Domain;
using GestiuneCD.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestiuneCD.Persistence
{
    public interface ICDsService<T> where T : BaseEntity
    {
        Task<T> GetItemByIdAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool? orderedByName = false, int? minSpatiuLiber = 0, int? vitezaDeInscriptionare = 0, TipCD? tipCD = null);
        Task<ActionResult<T>> UpdateItemAsync(int id, CDUpdateDTO entity);
        Task<ActionResult<T>> CreateItemAsync(CDSetupDTO entity);
        Task<ActionResult<T>> DeleteItemAsync(int id);
        Task<IEnumerable<T>> OrderBySize(string? orderType);
    }
}
