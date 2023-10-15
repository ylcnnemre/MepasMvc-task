namespace MepasTask.Abstract
{
    public interface IExcelWriteRepository
    {
        void CreateProductsWorkSheet();

        void CreateUsersWorkSheet();

        void CreateCategoryWorkSheet();

        void CreateAllWorkSheets();
    }
}
