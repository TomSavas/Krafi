using System;

namespace Krafi.UserInterface
{
    public interface IInputReader
    {
        string ReadStop();
        TimeSpan ReadTime();
        bool ReadDoMoreSearches();
    }
}