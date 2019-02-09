using System;
using Krafi.DataObjects;

namespace Krafi.UserInterface
{
    public interface IOutputWriter 
    {
        void WritePath(IPath path);
        void WriteElapsedTime(TimeSpan elapsedTime);
    }
}