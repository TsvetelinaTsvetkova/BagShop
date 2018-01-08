namespace BagShop.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BagShop.Data;
    using BagShop.Data.Models;
    using BagShop.Models.Bags;
    using System.Linq;
    using BagShop.Models.Models.Bags;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class BagService : IBagService
    {

        private const int PageSize = 25;

        private readonly BagShopDbContext db;

        public BagService(BagShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<BagListingModel> All(int page = 1)
        {
            return this.db
                 .Bags
                 .OrderByDescending(b => b.Id)
                 .Skip((page - 1) * PageSize)
                 .Take(PageSize)
                 .Select(b => new BagListingModel
                 {
                     Id = b.Id,
                     ImageUrl = b.ImageUrl,
                     Title = b.Title,
                     Color = b.Color,
                     Price = b.Price,
                     Description = b.Description
                 })
                 .ToList();
        }

        public void Create(string imageUrl, string title, string color, double price,
            string description, int quantity, string userId)
        {
            var bag = new Bag
            {
                ImageUrl = imageUrl,
                Title = title,
                Color = color,
                Price = price,
                Description = description,
                Quantity = quantity,
                UserId = userId
            };

            this.db.Add(bag);
            this.db.SaveChanges();
        }

        public bool BagExists(int? id)
        {
            if (id is null)
            {
                return false;
            }

            return this.db.Bags.Any(c => c.Id == id);
        }

        public void DeleteBag(int bagId)
        {
            var bag = this.db.Bags.First(c => c.Id == bagId);

            this.db.Remove(bag);

            this.db.SaveChanges();
        }

        public void EditBag(EditBagModel editBagModel)
        {
            var bag = this.db.Bags.First(c => c.Id == editBagModel.BagId);

            bag.ImageUrl = editBagModel.ImageUrl;
            bag.Color = editBagModel.Color;
            bag.Description = editBagModel.Description;
            bag.Price = editBagModel.Price;
            bag.Quantity = editBagModel.Quantity;
            bag.Title = editBagModel.Title;


            this.db.SaveChanges();
        }

        public EditBagModel GetBagCompleteEditInformation(int bagId)
        {
            var bag = this.db.Bags.First(c => c.Id == bagId);

            EditBagModel editBagModel = new EditBagModel()
            {
                BagId = bag.Id,
                Description = bag.Description,
                Color=bag.Color,
                ImageUrl = bag.ImageUrl,
                Price = bag.Price,
                Quantity = bag.Quantity,
                Title = bag.Title
            };

            return editBagModel;
        }

        public BagFullDetailsModel GetBagFullDetails(int? id)
        {
            var bag = this.db.Bags.First(c => c.Id == id.Value);
            this.db.Entry(bag).Reference(c => c.User).Load();

            var fullDetailsModel = new BagFullDetailsModel()
            {
                ImageUrl = bag.ImageUrl,
                Price = bag.Price,
                Color=bag.Color,
                Description=bag.Description,
                Quantity = bag.Quantity,
                Title = bag.Title,
            };

            return fullDetailsModel;
        }

        public IEnumerable<BagFullDetailsModel> GetAllBagsDetails()
        {
            var bags = this.db.Bags
                .Select(b => new BagFullDetailsModel()
                {
                    ImageUrl = b.ImageUrl,
                    BagId = b.Id,
                    Color=b.Color,
                    Description=b.Description,
                    Price = b.Price,
                    Quantity = b.Quantity,
                    Title = b.Title,
                }).ToList();

            return bags;
        }

        public async System.Threading.Tasks.Task<IEnumerable<BagListingServiceModel>> FindAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Bags
                .OrderByDescending(c => c.Id)
                .Where(c => c.Title.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<BagListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<BagListingServiceModel>> ActiveAsync()
      => await this.db
          .Bags
          .OrderByDescending(c => c.Id)
          .ProjectTo<BagListingServiceModel>()
          .ToListAsync();

    }
}
