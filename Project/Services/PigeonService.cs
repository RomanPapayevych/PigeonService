using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Data;
using Project.Enums;
using Project.ViewModels;
using System.Globalization;
using System.Linq;

namespace Project.Services
{
    public class PigeonService : IPigeonService
    {

        private readonly ApplicationDbContext? _context;

        public PigeonService(ApplicationDbContext? context)
        {
            _context = context;
        }
        
        public async Task AddPigeonAsync(PigeonDTO pigeonDTO)
        {
            //if (DateTime.TryParseExact(pigeonDTO.Year.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            //{
                var pigeon = new PigeonDTO
                {
                    Id = Guid.NewGuid(),
                    PigeonName = pigeonDTO.PigeonName,
                    Color = pigeonDTO.Color,
                    Number = pigeonDTO.Number,
                    Year = pigeonDTO.Year,
                    Gender = pigeonDTO.Gender,
                    IsAlive = pigeonDTO.IsAlive,
                    Father = pigeonDTO.Father,
                    Mother = pigeonDTO.Mother,
                    Description = pigeonDTO.Description, 
                    
                };
                _context?.Pigeons.Add(pigeon);
                await _context!.SaveChangesAsync();
            //}
            
        }
        
        public async Task<List<PigeonDTO>> GetAllPigeonByNameAsync(string pigeonName)
        {
            var pigeon = await _context!.Pigeons.Where(p => p.PigeonName == pigeonName).ToListAsync();
            return pigeon;
        }
        
        public async Task<IEnumerable<PigeonDTO>> GetAll()
        {
            var result = await _context!.Pigeons.Select(p => new PigeonDTO
            {
                Id = p.Id,
                PigeonName = p.PigeonName,
                Color = p.Color,
                Number = p.Number,
                Year = p.Year,
                Gender = p.Gender,
                IsAlive = p.IsAlive,
                Father = p.Father,
                Mother = p.Mother,
                //Father = p.Father != null ? new Guid(p.Father.ToString()!) : (Guid?) null,  
                //Mother = p.Mother != null ? new Guid(p.Mother.ToString()!) : (Guid?) null,
                Description = p.Description
            }).ToListAsync();
            return result;

        }

        public async Task DeletePigeonAsync(Guid pigeonId)
        {
            var pigeon = await _context!.Pigeons.FindAsync(pigeonId);
            if (pigeon != null)
            {
                _context.Pigeons.Remove(pigeon);
                await _context!.SaveChangesAsync();
            }
        }

        public async Task<PigeonDTO> GetPigeonByIdAsync(Guid pigeonId)
        {
            var pigeon = await _context!.Pigeons.FirstOrDefaultAsync(pigeon => pigeon.Id == pigeonId);
            return pigeon!;
        }

        public async Task UpdatePigeonAsync(PigeonDTO pigeonDTO)
        {
            var existingPigeon = await _context!.Pigeons.FindAsync(pigeonDTO.Id);
            if (existingPigeon != null)
            {
                existingPigeon.PigeonName = pigeonDTO.PigeonName;
                existingPigeon.Color = pigeonDTO.Color;
                existingPigeon.Number = pigeonDTO.Number;
                existingPigeon.Year = pigeonDTO.Year;
                existingPigeon.Gender = pigeonDTO.Gender;
                existingPigeon.Father = pigeonDTO.Father;
                existingPigeon.Mother = pigeonDTO.Mother;
                existingPigeon.IsAlive = pigeonDTO.IsAlive;
                existingPigeon.Description = pigeonDTO.Description;

                _context.Pigeons.Update(existingPigeon);
                await _context!.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetPigeonNumberAsync()
        {
            var pigeonNumber = await _context!.Pigeons.Select(p => p.Number).ToListAsync();
            return pigeonNumber!;
        }

        public async Task<List<PigeonDTO>> SearchPigeonAsync(string searchString, string searchBy)
        {
            //|| p.Gender.ToString().Contains(searchString) ||
            //p.IsAlive.ToString().Contains(searchString) ||
            var pigeons = await _context!.Pigeons.Where(p => p.PigeonName!.Contains(searchString) || p.Color!.Contains(searchString) || 
            p.Number!.Contains(searchString) || p.Year.ToString()!.Contains(searchString) /*|| p.Father!.Contains(searchString)*/ || 
           /* p.Mother!.Contains(searchString) ||*/ p.Description!.Contains(searchString)).ToListAsync();
            return pigeons!;
        }
        //public async Task<List<PigeonDTO>> GetPigeonLineageAsync(Guid id)
        //{
        //    var lineage = new List<PigeonDTO>();
        //    var pigeon = await _context!.Pigeons.FirstOrDefaultAsync(p => p.Id == id);
        //    await BuildLineage(pigeon!, lineage);
        //    return lineage;
        //}

        //private async Task BuildLineage(PigeonDTO pigeon, List<PigeonDTO> lineage)
        //{
        //    if (pigeon == null)
        //        return;

        //    lineage.Add(pigeon);

        //    if (!string.IsNullOrEmpty(pigeon.Father))
        //    {
        //        var father = await _context!.Pigeons.FirstOrDefaultAsync(p => p.PigeonName == pigeon.Father);
        //        await BuildLineage(father!, lineage);
        //    }

        //    if (!string.IsNullOrEmpty(pigeon.Mother))
        //    {
        //        var mother = await _context!.Pigeons.FirstOrDefaultAsync(p => p.PigeonName == pigeon.Mother);
        //        await BuildLineage(mother!, lineage);
        //    }
        //}
        //public async Task<List<PigeonDTO>> GetPigeonLineageAsync(Guid id)
        //{
        //    var lineage = new List<PigeonDTO>();
        //    var pigeon = await _context!.Pigeons.FindAsync(id);
        //    await BuildLineage(pigeon!, lineage);
        //    return lineage;
        //}

        //private async Task BuildLineage(PigeonDTO pigeon, List<PigeonDTO> lineage)
        //{
        //    if (pigeon == null)
        //        return;

        //    lineage.Add(pigeon);

        //    // Шукаємо батька голуба та додаємо його до родоводу
        //    if (pigeon.Father != null)
        //    {
        //        var father = await _context!.Pigeons.FindAsync(pigeon.Father);
        //        await BuildLineage(father!, lineage);
        //    }

        //    // Шукаємо матір голуба та додаємо її до родоводу
        //    if (pigeon.Mother != null)
        //    {
        //        var mother = await _context!.Pigeons.FindAsync(pigeon.Mother);
        //        await BuildLineage(mother!, lineage);
        //    }
        //}

        public async Task<List<Guid>> GetPigeonForParentIdAsync()
        {
            var parentId = await _context!.Pigeons.Select(p => p.Id).ToListAsync();
            return parentId!;
        }

        public async Task<List<string>> GetPigeonForNumberAsync(List<Guid> pigeonForId)
        {
            var number = await _context!.Pigeons.Select(p => p.Number).ToListAsync();
            return number!;
        }
        public async Task<PigeonDTO> GetPigeonByNumberAsync(string pigeonNumber)
        {
            var pigeon = await _context!.Pigeons.FirstOrDefaultAsync(p => p.Number == pigeonNumber);
            return pigeon!;
        }

        //public async Task<List<PigeonDTO>> GetLineageAsync(Guid Id)
        //{
        //    var lineage = new List<PigeonDTO>();
        //    await GetLineageRecursiveAsync(Id, lineage);
        //    return lineage;
        //}

        //private async Task GetLineageRecursiveAsync(Guid Id, List<PigeonDTO> lineage)
        //{
        //    var pigeon = await _context!.Pigeons.FindAsync(Id);
        //    if (pigeon != null)
        //    {
        //        lineage.Add(pigeon);

        //        if (pigeon.Father != null)
        //        {
        //            await GetLineageRecursiveAsync(pigeon.Father.Value, lineage);
        //        }

        //        if (pigeon.Mother != null)
        //        {
        //            await GetLineageRecursiveAsync(pigeon.Mother.Value, lineage);
        //        }
        //    }
        //}
    }
}
