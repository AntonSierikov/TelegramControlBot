using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using ControlBot.Core.Constants;

namespace ControlBot.BL.ReplyMarkups
{
    public static class MarkupBuilder
    {

        //----------------------------------------------------------------//

        public static InlineKeyboardMarkup CaseNotifyKeyboardMarkup(Int32 historyId)
        {
            InlineKeyboardButton yesButton = new InlineKeyboardButton() { Text = CommonConstants.YES, CallbackData = historyId.ToString() };
            InlineKeyboardButton noButton = new InlineKeyboardButton() { Text = CommonConstants.NO };

            return new InlineKeyboardMarkup(new[] { yesButton, noButton });
        }

        //----------------------------------------------------------------//

    }
}
