using ControlBot.DAL.Abstract;
using System;
using System.Threading.Tasks;
using ControlBot.BL.Models;

namespace ControlBot.BL.IServices
{
    public interface ISequencerService
    {
        Task<ProcessResult> TryGenerateOrUpdateSequence(ISession session, String nameCase, String[] userNames);
    }
}
