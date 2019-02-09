using System;

namespace Krafi.UserInterface
{
    public interface IInputReader
    {
        string ReadStop();
        DateTime ReadTime();
        bool ReadDoMoreSearches();
    }
}