using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using NSI.Repository.Interfaces;

namespace NSI.BLL
{
    public class MeetingsManipulation : IMeetingsManipulation
    {
        private readonly IMeetingsRepository _meetingsRepository;

        public MeetingsManipulation(IMeetingsRepository meetingsRepository)
        {
            _meetingsRepository = meetingsRepository;
        }

        public MeetingDto Create(MeetingDto model)
        {
            return _meetingsRepository.Insert(model);
        }
    }
}
