using System;
using ControlBot.BL.TelegramCommands;

namespace ControlBot.BL.Abstract
{
    public interface ITelegramCommandFactory
    {
        BaseTelegramCommand GetTelegramCommand(String command);
    }
}
