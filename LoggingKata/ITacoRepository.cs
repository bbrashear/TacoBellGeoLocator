using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingKata
{
    interface ITacoRepository
    {
        void CreateTacoBells(ITrackable tacoBell);

        IEnumerable<ITrackable> GetAllTacoBells();
    }
}
