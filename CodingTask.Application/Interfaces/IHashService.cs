using System.Threading.Tasks;

namespace CodingTask.Application.Interfaces
{
    public interface IHashService
    {
        Task<string> HashText(string plainText);
    }
}
