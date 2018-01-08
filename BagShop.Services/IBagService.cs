using BagShop.Models.Bags;
using BagShop.Models.Models.Bags;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BagShop.Services
{


    public interface IBagService
    {
        void Create(
              string ImageUrl,
              string Title,
              string Color,
              double Price,
              string Description,
              int Quantity,
              string UserID);

        IEnumerable<BagListingModel> All(int page = 1);

        Task<IEnumerable<BagListingServiceModel>> ActiveAsync();

        bool BagExists(int? id);

        void EditBag(EditBagModel editCameraModel);

        void DeleteBag(int cameraId);

        EditBagModel GetBagCompleteEditInformation(int bagId);

        BagFullDetailsModel GetBagFullDetails(int? id);

        IEnumerable<BagFullDetailsModel> GetAllBagsDetails();

        Task<IEnumerable<BagListingServiceModel>> FindAsync(string searchText);

    }
}
