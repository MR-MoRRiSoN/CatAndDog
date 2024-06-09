using GiviQiria.final.DB;
using GiviQiria.final.Models;
using GiviQiria.final.Models.DTO;

namespace GiviQiria.final.Service
{
    public class MainPageService : IMainPageService
    {
        private readonly IConfiguration _configuration;

        public MainPageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public UpdateCatRequest GetCatById(Guid id)
        {
            UpdateCatRequest newCatRequest = new UpdateCatRequest();
            using (var context = new AppDbContext(_configuration))
            {
                var cat = context.Cats.Where(catItem => catItem.CatId == id).FirstOrDefault();
                if (cat != null)
                {

                    newCatRequest = new UpdateCatRequest
                    {
                        CatId = cat.CatId,
                        BirthDate = cat.BirthDate,
                        Gender = cat.Gender,
                        Name = cat.Name,
                        Varieties = cat.Varieties,
                        Weight = cat.Weight,
                    };
                }

            }

            return newCatRequest;

        }

        public UpdateToyRequest GetToyById(Guid id)
        {
            UpdateToyRequest updateToyRequest = new UpdateToyRequest();
            using (var context = new AppDbContext(_configuration))
            {
                var toy = context.Toys.Where(toyItem => toyItem.ToyId == id).FirstOrDefault();
                if (toy != null)
                {

                    updateToyRequest = new UpdateToyRequest
                    {
                        ToyId = toy.ToyId,
                        ToyName = toy.ToyName,
                        Color = toy.Color,
                        Weight = toy.Weight,

                    };
                }

            }

            return updateToyRequest;

        }


        public List<GetCats> GetCatList()
        {
            List<GetCats> getCatList = new List<GetCats>();


            using (var context = new AppDbContext(_configuration))
            {
                context.Database.EnsureCreated();
                var catList = context.Cats.ToList();
                getCatList = catList.Select(cat => new GetCats
                {
                    CatId = cat.CatId,
                    Name = cat.Name,
                    BirthDate = cat.BirthDate,
                    Gender = cat.Gender,
                    Varieties = cat.Varieties,
                    Weight = cat.Weight
                }).ToList();
            }

            return getCatList;
        }

        public List<GetCatToy> GetCatToyList(Guid catId)
        {
            List<GetCatToy> toys = new List<GetCatToy>();

            using (var context = new AppDbContext(_configuration))
            {
                context.Database.EnsureCreated();
                var toyList = context.Toys.Where(toy => toy.ToyOwnerId == catId).ToList();
                toys = toyList.Select(toy => new GetCatToy
                {
                    ToyId = toy.ToyId,
                    ToyName = toy.ToyName,
                    Weight = toy.Weight,
                    Color = toy.Color
                }).ToList();


            }
            return toys;
        }


        public void SaveCat(AddNewCatRequest addNewCat)
        {
            using (var context = new AppDbContext(_configuration))
            {
                var cat = new Cat
                {
                    CatId = Guid.NewGuid(),
                    Name = addNewCat.Name,
                    BirthDate = addNewCat.BirthDate,
                    Gender = addNewCat.Gender,
                    Varieties = addNewCat.Varieties,
                    Weight = addNewCat.Weight,
                };
                context.Add(cat);
                context.SaveChanges();
            }

        }
        public void SaveCatToy(AddNewCatToy addNewCatToy)
        {
            using (var context = new AppDbContext(_configuration))
            {
                var cat = context.Cats.SingleOrDefault(c => c.CatId == addNewCatToy.CatId);

                if (cat == null)
                {
                    throw new Exception("The specified CatId does not exist.");
                }

                var catToy = new Toy
                {
                    ToyId = Guid.NewGuid(),
                    ToyName = addNewCatToy.ToyName,
                    Weight = addNewCatToy.Weight,
                    Color = addNewCatToy.Color,
                    ToyOwnerId = addNewCatToy.CatId,
                    ToyOwner = cat
                };

                context.Toys.Add(catToy);
                context.SaveChanges();
            }
        }

        public void UpdateToy(UpdateToyRequest updateToy)
        {
            using (var context = new AppDbContext(_configuration))
            {

                var toy = context.Toys.SingleOrDefault(t => t.ToyId == updateToy.ToyId);
                if (toy != null)
                {
                    toy.Weight = updateToy.Weight;
                    toy.Color = updateToy.Color;
                    toy.ToyName = updateToy.ToyName;

                    context.Toys.Update(toy);

                    context.SaveChanges();
                }


            }
        }

        public void UpdateCat(UpdateCatRequest updateCat)
        {
            using (var context = new AppDbContext(_configuration))
            {

                var cat = context.Cats.SingleOrDefault(cat => cat.CatId == updateCat.CatId);
                if (cat != null)
                {

                    cat.Weight = updateCat.Weight;
                    cat.Name = updateCat.Name;
                    cat.BirthDate = updateCat.BirthDate;
                    cat.Gender = updateCat.Gender;
                    cat.Varieties = updateCat.Varieties;


                    context.Cats.Update(cat);

                    context.SaveChanges();
                }


            }
        }


        public void DeleteCat(List<Guid> catIds)
        {
            using (var context = new AppDbContext(_configuration))
            {
                foreach (var catId in catIds ?? new List<Guid>())
                {
                    var cat = context.Cats.SingleOrDefault(c => c.CatId == catId);
                    if (cat != null)
                    {
                        List<Guid> catIdsToRemove = new List<Guid>();

                        foreach (var deleteToy in cat.CatToys ?? [])
                        {
                            catIdsToRemove.Add(deleteToy.ToyId);
                        }

                        DeleteCatToy(catIdsToRemove);
                        context.Cats.Remove(cat);
                    }
                }
                context.SaveChanges();
            }
        }

        public void DeleteCatToy(List<Guid> toyIds)
        {
            using (var context = new AppDbContext(_configuration))
            {
                foreach (var toyId in toyIds ?? new List<Guid>())
                {
                    var toy = context.Toys.SingleOrDefault(t => t.ToyId == toyId);
                    if (toy != null)
                    {
                        context.Toys.Remove(toy);
                    }
                }
                context.SaveChanges();
            }
        }

    }
}
