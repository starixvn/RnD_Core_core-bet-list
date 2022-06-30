namespace Fanex.BetList.Core.Entities
{
    using NPOI.SS.UserModel;

    /// <summary>
    /// Class TextBlock.
    /// </summary>
    public class TextBlock
    {
        private readonly string _text;

        private readonly IFont _font;

        public TextBlock(string text)
        {
            _text = text;
        }

        public TextBlock(string text, IFont font)
        {
            _text = text;
            _font = font;
        }

        public string Text
        {
            get { return _text; }
        }

        public IFont Font
        {
            get { return _font; }
        }
    }
}