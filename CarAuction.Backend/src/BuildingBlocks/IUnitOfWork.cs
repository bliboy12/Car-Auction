public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(); // returns an int value that indicates how many tracked entries were written during the save operation (mimicing Dbcontext.SaveChangesAsync())
}