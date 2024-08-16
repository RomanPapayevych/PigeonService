using Project.ViewModels;

namespace Project.Services
{
    public interface IPigeonService
    {
        Task AddPigeonAsync(PigeonDTO pigeonDTO);
        Task <List<PigeonDTO>>GetAllPigeonByNameAsync(string pigeonName);
        Task<PigeonDTO> GetPigeonByIdAsync(Guid pigeonId);
        //--------------------------
        Task<List<string>> GetPigeonNumberAsync();
        Task<List<Guid>> GetPigeonForParentIdAsync();
        Task<List<string>> GetPigeonForNumberAsync(List<Guid> pigeonForId);
        Task<PigeonDTO> GetPigeonByNumberAsync(string pigeonNumber);
        //--------------------------

        Task <IEnumerable<PigeonDTO>> GetAll();
        Task DeletePigeonAsync(Guid pigeonId);
        Task UpdatePigeonAsync(PigeonDTO pigeonDTO);
        Task<List<PigeonDTO>> SearchPigeonAsync(string searchString, string searchBy);
        //Task<List<PigeonDTO>> GetPigeonLineageAsync(Guid Id);
        //Task<List<PigeonDTO>> GetLineageAsync(Guid Id);


    }
}
