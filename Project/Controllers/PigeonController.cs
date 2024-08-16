using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    [Route("[controller]/[action]")]
    public class PigeonController : Controller
    {

        private readonly IPigeonService? _pigeonService;
        public PigeonController(IPigeonService? pigeonService)
        {
            _pigeonService = pigeonService;
        }


        [HttpGet]
        public async Task<IActionResult> AddPigeon()
        {
            //var pigeonNumbers = await _pigeonService!.GetPigeonNumberAsync();
            //ViewBag.PigeonNumbers = pigeonNumbers;
            var pigeonForId = await _pigeonService!.GetPigeonForParentIdAsync();
            var num = await _pigeonService!.GetPigeonForNumberAsync(pigeonForId);
            ViewBag.PigeonForId = num!;
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> AddPigeon(PigeonDTO pigeonDTO)
        {
            //var fatherPigeon = await _pigeonService!.GetPigeonByNumberAsync(pigeonDTO.Number!);
            //if (fatherPigeon != null)
            //{
            //    pigeonDTO.Father = fatherPigeon.Id;
            //}
            if (ModelState.IsValid)
            {
                await _pigeonService!.AddPigeonAsync(pigeonDTO);
            }
            return RedirectToAction("GetAll");

        }
            
        public async Task<IActionResult> GetAll(string searchString, string searchBy)
        {
            var pigeons = await _pigeonService!.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                pigeons = await _pigeonService!.SearchPigeonAsync(searchString, searchBy);
                return View(pigeons);
            }
            return View(pigeons);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            if (ModelState.IsValid)
            {
                await _pigeonService!.DeletePigeonAsync(Id);
            }
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Edit(Guid id)
        {

            //var pigeonNumbers = await _pigeonService!.GetPigeonNumberAsync();
            //ViewBag.PigeonNumbers = pigeonNumbers;
            var pigeonForId = await _pigeonService!.GetPigeonForParentIdAsync();
            var num = await _pigeonService!.GetPigeonForNumberAsync(pigeonForId);
            ViewBag.PigeonForId = num!;

            //var parsedGuid = Guid.Parse(id);
            PigeonDTO pigeonDTO = await _pigeonService!.GetPigeonByIdAsync(id);
            if (pigeonDTO == null)
            {
                return NotFound();
            }
            return View(pigeonDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit2([FromForm] PigeonDTO pigeonDTO)
        {
            //var pigeonNumbers = await _pigeonService!.GetPigeonNumberAsync();
            //ViewBag.PigeonNumbers = pigeonNumbers;
            var pigeonForId = await _pigeonService!.GetPigeonForParentIdAsync();
            var num = await _pigeonService!.GetPigeonForNumberAsync(pigeonForId);
            ViewBag.PigeonForId = num!;

            if (ModelState.IsValid)
            {
                await _pigeonService!.UpdatePigeonAsync(pigeonDTO);
                return RedirectToAction(nameof(GetAll));
            }
            return View(pigeonDTO);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Lineage(Guid id)
        {
            
            return View();
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Lineage(Guid id)
        //{
        //    var lineage = await _pigeonService!.GetPigeonLineageAsync(id);
        //    return View(lineage);
        //}
        //[HttpGet("{Id?}")]
        //public async Task<IActionResult> Lineage(Guid Id)
        //{
        //    var pigeon = await _pigeonService!.GetPigeonByIdAsync(Id);
        //    if (pigeon == null)
        //    {
        //        return NotFound();
        //    }

        //    // Fetch lineage using GetPigeonLineageAsync
        //    pigeon.Lineage = await _pigeonService!.GetPigeonLineageAsync(Id);

        //    return View("GetLineage", pigeon); // Assuming you want to display lineage within GetAll view
        //}
        //public async Task<IActionResult> GetLineage(Guid pigeonId)
        //{
        //    //var lineage = await _pigeonService!.GetPigeonLineageAsync(pigeonId);
        //    //ViewBag.Lineage = lineage;
        //    //return Content(lineage);
        //    //return View();
        //    var lineage = await _pigeonService!.GetPigeonLineageAsync(pigeonId);
        //    return View(lineage);
        //}

    }
}
