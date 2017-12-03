using NSI.DC.HearingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IHearingsRepository
    {
        void Insert(HearingDto Model);
    }
}
