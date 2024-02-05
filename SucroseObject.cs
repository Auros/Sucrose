using UnityEngine;

namespace Sucrose
{
    public abstract class SucroseObject
    {
        // ReSharper disable once InconsistentNaming
        protected readonly SucroseContainer _sucrose;

        public SucroseContainer Sucrose => _sucrose;

        internal SucroseObject(SucroseContainer sucrose)
        {
            _sucrose = sucrose;
        }
    }
}