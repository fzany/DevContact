using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevContact.Models
{
    //Holds the various stacks a developer can belong.
    public enum Stack
    {
        Frontend, Backend, Fullstack
    }

    /// <summary>
    /// Holds various platforms a developer can develop for.
    /// </summary>
    public enum Platform
    {
        Mobile, Web, Desktop, Cloud
    }

    public enum Sex
    {
        Male, Female
    }
}
