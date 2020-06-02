using System.Threading.Tasks;

namespace RADProject.HashFunctions {
    public interface IHashFunction {
        ulong Hash(ulong x);
    }
}