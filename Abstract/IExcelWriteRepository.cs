namespace MepasTask.Abstract
{
    public interface IExcelWriteRepository
    {

        public bool IsExcelOpen();

        void CreateProductsWorkSheet();

        void CreateUsersWorkSheet();

        void CreateCategoryWorkSheet();

    }
}
