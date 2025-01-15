namespace Fanex.BetList.Core.Utils
{
    using System.Collections.Generic;
    using Entities;
    using BetListNPOI.HSSF.UserModel;
    using BetListNPOI.SS.UserModel;

    public interface IRtfTextRender
    {
        void Clear();
        IRichTextString Render();
        void AddText(TextBlock block);
        void AddText(string text);
        void AddText(string text, IFont font);
    }
}