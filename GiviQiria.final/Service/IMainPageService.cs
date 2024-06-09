using GiviQiria.final.Models.DTO;

namespace GiviQiria.final.Service
{
    public interface IMainPageService
    {
        public List<GetCats> GetCatList();
        public List<GetCatToy> GetCatToyList(Guid catId);
        public void SaveCat(AddNewCatRequest addNewCat);
        public void SaveCatToy(AddNewCatToy addNewCatToy);
        public void DeleteCat(List<Guid> catId);
        public void DeleteCatToy(List<Guid> catId);
        public UpdateToyRequest GetToyById(Guid id);
        public UpdateCatRequest GetCatById(Guid id);
        public void UpdateToy(UpdateToyRequest updateToy);
        public void UpdateCat(UpdateCatRequest updateCat);
    }
}